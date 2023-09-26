using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private CharacterInfoSO characterInfoSO;
    [SerializeField] private Canvas unitUICanvas;

    private Dictionary<BufType, ABBuff> buffEffect = new Dictionary<BufType, ABBuff>();
    public Dictionary<BufType, ABBuff> BuffEffect
    {
        get
        {
            return buffEffect;
        }
        set
        {
            buffEffect = value;
        }
    }
    public BuffUIUpdate BuffUIUpdate
    {
        get
        {
            buffUIUpdate ??= unitUICanvas.GetComponentInChildren<BuffUIUpdate>();
            return buffUIUpdate;
        }
        set
        {
            buffUIUpdate ??= unitUICanvas.GetComponentInChildren<BuffUIUpdate>();
            buffUIUpdate = value;
        }
    }
    public ShieldUI ShieldUI
    {
        get
        {
            shieldUI ??= unitUICanvas.GetComponentInChildren<ShieldUI>();
            return shieldUI;
        }
        set
        {
            shieldUI ??= unitUICanvas.GetComponentInChildren<ShieldUI>();
            shieldUI = value;
        }
    }
    
    protected int maxHp => characterInfoSO.hp;

    protected int currentHp;
    private int shieldCount = 0;
    public int ShieldCount
    {
        get
        {
            return shieldCount;
        }
        set
        {
            shieldCount = Mathf.Max(value, 0);
            ShieldUI.UpdateShieldCountUI(shieldCount);
        }
    }
    public int CurrentHP
    {
        get => currentHp;
        set
        {
            currentHp = value;
            unitUICanvas.GetComponentInChildren<UnitHpEffect>().SetHPBar(maxHp, currentHp);
        }
    }
    
    public CharacterInfoSO CharacterInfoSo => characterInfoSO;
    public Unit enemy;

    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;

    private BuffUIUpdate buffUIUpdate;
    private ShieldUI shieldUI;
    
    protected GameObject visual;

    private string anme;

    #region TurnEffect

    
    public float MyTurnStart()
    {
        float _time = BuffEffect.Sum(_variable => _variable.Value.TurnStart());
        CheckBuff();
        return _time;
    }
    public float MyTurnEnd()
    {
        float _time = BuffEffect.Sum(_variable => _variable.Value.TurnEnd());
        CheckBuff();
        return _time;
    }
    private int AttackEffect(int _damage)
    {
        int _addDamage = BuffEffect.Sum(_variable => _variable.Value.AttackEffect(_damage));
        CheckBuff();
        return _addDamage;
    }
    private int HitEffect(int _damage)
    {
        int _addDamage = BuffEffect.Sum(_variable => _variable.Value.HitEffect(_damage));
        CheckBuff();
        return _addDamage;
    }
    private int HealEffect(int _amount)
    {
        int _addHeal = BuffEffect.Sum(_variable => _variable.Value.HealEffect(_amount));
        CheckBuff();
        return _addHeal;
    }
    private void CheckBuff()
    {
        List<BufType> _removeBuf = BuffEffect.Keys.ToList();

        for (int i = 0; i < _removeBuf.Count; i++)
        {
            if (BuffEffect[_removeBuf[i]].Count <= 0)
                RemoveBuff(_removeBuf[i]);
        }
    }


    #endregion
    private void Update()
    {
        foreach (var _variable in BuffEffect.Values)
        {
            _variable.Update();
        }
    }

    protected void SetInfo()
    {
        animator ??= GetComponentInChildren<Animator>();
        visual ??= transform.Find("Visual").gameObject;

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        if (characterInfoSO.skillName != "")
        {
            gameObject.AddComponent(Type.GetType(characterInfoSO.skillName));
        }

        SetAnim();
    }

    public void SetUnitCanvas()
    {
        unitUICanvas = PoolManager.Pop(PoolType.UnitCanvas).GetComponent<Canvas>();
        unitUICanvas.transform.SetParent(transform);
        unitUICanvas.transform.localPosition = Vector3.zero;
        unitUICanvas.transform.localScale = new Vector3(0.0021f, 0.0021f, 0.0021f);

        CurrentHP = maxHp;
    }

    private void SetAnim()
    {
        animatorOverrideController["Attack"] = characterInfoSO.atkAnim;
        animatorOverrideController["Skill"] = characterInfoSO.skillAnim;
        animatorOverrideController["Die"] = characterInfoSO.dieAnim;
        animatorOverrideController["Hit"] = characterInfoSO.hitAnim;
        animatorOverrideController["Block"] = characterInfoSO.blockAnim;
        animatorOverrideController["Run"] = characterInfoSO.runAnim;
        animatorOverrideController["Idle"] = characterInfoSO.idleAnim;
    }

    public virtual float Attack(int _damage)
    {
        animator.SetTrigger("Attack");

        _damage += AttackEffect(_damage);
        enemy.Hit(_damage);
        
        return animatorOverrideController["Attack"].length;
    }

    public virtual float Hit(int _damage, bool _trueDamage = false)
    {
        animator.SetTrigger("Hit");

        _damage = DamageCalculation(_damage, _trueDamage);

        int _hp = CurrentHP - _damage;
        CurrentHP = Mathf.Max(0, _hp);

        HitFeedback(_damage);

        if (CurrentHP <= 0)
        {
            animator.SetTrigger("Die");
            MapPlayerTracker.Instance.StartBattleCoru(false, 1.5f);
        }

        return animatorOverrideController["Hit"].length;
    }

    public virtual float Heal(int _amount, bool _isAnim = true)
    {
        _amount += HealEffect(_amount);
        
        int _healHp = Mathf.Min(maxHp, CurrentHP + _amount);
        CurrentHP = _healHp;

        if (!_isAnim) return 0;
        animator.SetTrigger("Skill");
        return animatorOverrideController["Skill"].length;
    }

    public void Run()
    {
        animator.SetTrigger("Run");
    }

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }
    
    public int DamageCalculation(int _damage, bool _trueDamage)
    {
        if (_trueDamage)
            return _damage;
        
        _damage += HitEffect(_damage);

        _damage -= ShieldCount;
        if (_damage >= 0)
            ShieldCount -= _damage;

        return _damage < 0 ? 0 : _damage;
    }

    public virtual float SpecialAbility()
    {
        animator.SetTrigger("Skill");
        
        return animatorOverrideController["Skill"].length;
    }

    public void AddBuff(BuffDataSO _buffDataSO, int _count)
    {
        if (BuffEffect.ContainsKey(_buffDataSO.bufType))
        {
            BuffEffect[_buffDataSO.bufType].AddBuffCount(_count);
            return;
        }

        BuffEffect.Add(_buffDataSO.bufType, BuffManager.Instance.BuffType(this, _buffDataSO, _count));
    }

    public void RemoveBuff(BufType _bufType)
    {
        if (!BuffEffect.ContainsKey(_bufType)) return;

        buffUIUpdate.RemoveBuffUI(_bufType);
        BuffEffect.Remove(_bufType);
    }

    private void HitFeedback(int _damage)
    {
        DamageTextManager.CreateDamageText(transform.position, _damage, Color.red);
        visual.transform.DOShakePosition(0.8f, Mathf.Clamp(_damage / 80f, 0.04f, 0.5f), 70).SetUpdate(true);
        EffectManager.Instance.TriggerShake(0.2f, 0.2f, 0.2f);
    }
}

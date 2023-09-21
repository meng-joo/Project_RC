using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

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
    protected int maxHp => characterInfoSO.hp;

    protected int currentHp;

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
    
    protected GameObject visual;

    private string anme;

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

    private void CheckBuff()
    {
        List<BufType> _removeBuf = (from _variable in BuffEffect where _variable.Value.Count <= 0 select _variable.Key).ToList();

        for (int i = 0; i < _removeBuf.Count - 1; i++)
        {
            RemoveBuff(_removeBuf[i]);
        }
    }

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

        unitUICanvas = PoolManager.Pop(PoolType.UnitCanvas).GetComponent<Canvas>();
        unitUICanvas.transform.SetParent(transform);
        unitUICanvas.transform.localPosition = Vector3.zero;
        unitUICanvas.transform.localScale = new Vector3(0.0021f, 0.0021f, 0.0021f);

        CurrentHP = maxHp;
        
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        if (characterInfoSO.skillName != "")
        {
            gameObject.AddComponent(Type.GetType(characterInfoSO.skillName));
        }

        SetAnim();
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

    public virtual float Hit(int _damage)
    {
        animator.SetTrigger("Hit");

        _damage += HitEffect(_damage);
        int _hp = CurrentHP - _damage;
        CurrentHP = Mathf.Max(0, _hp);

        HitFeedback(_damage);

        if (CurrentHP <= 0)
        {
            
        }

        return animatorOverrideController["Hit"].length;
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
    }
}

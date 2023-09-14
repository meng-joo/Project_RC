using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private CharacterInfoSO characterInfoSO;
    [SerializeField] private Canvas unitUICanvas;

    private Dictionary<BuffDataSO, int> buffDataList = new Dictionary<BuffDataSO, int>();
    public Dictionary<BuffDataSO, int> BuffDataList
    {
        get
        {
            return buffDataList;
        }
        set
        {
            buffDataList = value;
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
        
        return animatorOverrideController["Attack"].length;
    }

    public virtual float Hit(int _damage)
    {
        animator.SetTrigger("Hit");
        
        int _hp = CurrentHP - _damage;
        CurrentHP = Mathf.Max(0, _hp);
        
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
        if (BuffDataList.ContainsKey(_buffDataSO))
        {
            BuffDataList[_buffDataSO] += _count;
            BuffUIUpdate.UpdateBuffUI(_buffDataSO, BuffDataList[_buffDataSO], false);
            return;
        }

        BuffDataList.Add(_buffDataSO, _count);
        BuffUIUpdate.UpdateBuffUI(_buffDataSO, _count, true);
    }
}

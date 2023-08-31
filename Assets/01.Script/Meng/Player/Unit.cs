using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private CharacterInfoSO characterInfoSO;
    public CharacterInfoSO CharacterInfoSo => characterInfoSO;
    
    protected Animator animator;
    protected AnimatorOverrideController animatorOverrideController;
    
    protected GameObject visual;

    private string anme;

    private void Start()
    {
        SetInfo();
    }

    private void SetInfo()
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

    public virtual void Attack(int _damage)
    {
        animator.SetTrigger("Attack");
    }

    public virtual void Hit(int _damage)
    {
        animator.SetTrigger("Hit");
    }

    public virtual void SpecialAbility()
    {
        animator.SetTrigger("Skill");
    }
}

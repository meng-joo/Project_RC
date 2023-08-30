using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    public CharacterInfoSO characterInfoSO;

    public UnityEvent hpChangEvent;
    public virtual void Hit()
    {

    }
    public virtual void Attack()
    {

    }

    public void ChangeHp()
    {

        hpChangEvent.Invoke();
    }

    public void SetHPUI()
    {
        //HP에 맞게 슬라이더 조정
    }

    public virtual void SpecialAbility()
    {
        Debug.Log("특수능력");
    }
}

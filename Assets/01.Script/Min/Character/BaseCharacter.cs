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
        //HP�� �°� �����̴� ����
    }

    public virtual void SpecialAbility()
    {
        Debug.Log("Ư���ɷ�");
    }
}

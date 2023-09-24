using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    public void Start()
    {
        SetInfo();

        ShieldCount = 0;
        enemy = FindObjectOfType<Enemy>();
    }
    
    public override float Attack(int _damage)
    {
        return base.Attack(_damage);
    }
}

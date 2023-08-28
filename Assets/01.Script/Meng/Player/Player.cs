using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void Attack(int _damage)
    {
        animator.SetTrigger("Attack");
    }
}

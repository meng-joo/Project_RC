using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Weak : ABBuff
{
    public Debuff_Weak(Unit _unit, BuffDataSO _buffDataSO, int _count) : base(_unit, _buffDataSO, _count)
    {
    }

    public override void Start()
    {
        
    }

    public override float TurnStart()
    {
        return 0;
    }

    public override void Update()
    {
        
    }

    public override float TurnEnd()
    {
        AddBuffCount(-1);
        return 0;
    }

    public override int AttackEffect(int _damage)
    {
        _damage = Mathf.RoundToInt(_damage * 0.25f);
        return -_damage;
    }

    public override int HitEffect(int _damage)
    {
        return 0;
    }

    public override int HealEffect(int _amount)
    {
        return 0;
    }

    public override void End()
    {
        
    }
}

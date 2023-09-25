using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Thorn : ABBuff
{
    public Buff_Thorn(Unit _unit, BuffDataSO _buffDataSO, int _count) : base(_unit, _buffDataSO, _count)
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
        return 0;
    }

    public override int AttackEffect(int _damage)
    {
        return 0;
    }

    public override int HitEffect(int _damage)
    {
        unit.enemy.Hit(Count);
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

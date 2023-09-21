using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Poison : ABBuff
{
    public Debuff_Poison(Unit _unit, BuffDataSO _buffDataSO, int _count) : base(_unit, _buffDataSO, _count)
    {
    }

    public override void Start()
    {
        
    }

    public override float TurnStart()
    {
        float _time = unit.Hit(Count);
        
        return _time;
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
        return 0;
    }

    public override int HitEffect(int _damage)
    {
        return 0;
    }

    public override void End()
    {
        
    }
}

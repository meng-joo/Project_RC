using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABBuff
{
    protected Unit unit;
    protected BuffDataSO buffDataSO;
    public int Count { get; private set; }

    public ABBuff(Unit _unit, BuffDataSO _buffDataSO, int _count)
    {
        unit = _unit;
        buffDataSO = _buffDataSO;
        AddBuffCount(_count);
        Start();
    }

    public abstract void Start();
    public abstract float TurnStart();
    public abstract void Update();
    public abstract float TurnEnd();
    public abstract int AttackEffect(int _damage);
    public abstract int HitEffect(int _damage);
    public abstract void End();

    public void AddBuffCount(int _addCount)
    {
        Count += _addCount;
        Count = Mathf.Max(Count, 0);

        unit.BuffUIUpdate.UpdateBuffUI(buffDataSO, Count);
    }
}

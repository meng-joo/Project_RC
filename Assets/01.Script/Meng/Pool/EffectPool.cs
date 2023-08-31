using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : PoolAbleObject
{
    [SerializeField] private PoolType poolType;
    [SerializeField] private float deley;
    public override void Init_Pop()
    {
        Invoke(nameof(PushObject), deley);
    }

    private void PushObject()
    {
        PoolManager.Push(poolType, gameObject);
    }

    public override void Init_Push()
    {
        
    }
}

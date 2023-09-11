using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectPool : PoolAbleObject
{
    [SerializeField] private float deley;
    public override void Init_Pop()
    {
        Invoke(nameof(PushObject), deley);
    }

    private void PushObject()
    {
        PoolManager.Push(PoolType, gameObject);
    }

    public override void Init_Push()
    {
        
    }
}

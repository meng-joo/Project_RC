using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : PoolAbleObject
{
    private AbCard card => GetComponentInChildren<AbCard>();
    
    public override void Init_Pop()
    {
        card.Init_Pop();
    }

    public override void Init_Push()
    {
        card.Init_Push();
    }
}

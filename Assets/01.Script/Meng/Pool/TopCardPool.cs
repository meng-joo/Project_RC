using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCardPool : PoolAbleObject
{
    private AbCard _card;
    
    public override void Init_Pop()
    {
        
    }

    public override void Init_Push()
    {
        
    }

    public void SetCardInfo(CardSO _cardSO)
    {
        _card ??= GetComponentInChildren<AbCard>();
        _card.CardSO = _cardSO;
    }
}

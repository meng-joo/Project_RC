using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPool : PoolAbleObject
{
    private AbCard abCard;
    
    public override void Init_Pop()
    {
        abCard ??= GetComponentInChildren<AbCard>();
        abCard.InitializationTransform();
    }

    public override void Init_Push()
    {
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        abCard ??= GetComponentInChildren<AbCard>();
        abCard.CardSO = _cardSO;
    }
}

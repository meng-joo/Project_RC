using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public CardTier CardTier;
    public CardSO CardSO;
    //private CardTier cardTier;
    //public CardTier CardTier
    //{
    //    get => cardTier;
    //    set { cardTier = value; }
    //}

    public void SetData(CardSO cardSO)
    {
        CardSO = cardSO;
    }

}

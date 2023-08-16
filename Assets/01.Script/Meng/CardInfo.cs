using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTier
{
    R,
    SR,
    SSR,
    NONE
}

public enum CardType
{
    ATK,
    DEF,
    BUF,
    NONE
}

[Serializable]
public class CardInfo
{
    public string name;

    public PoolType cardPoolType;
    
    public CardTier cardTier;
    public CardType cardType;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CardSO")]
public class CardSO : ScriptableObject
{
    public CardInfo cardInfo;
    public CardInfo upgradeCardInfo;
    public CardInfo transcendenceCardInfo;
    
    public Sprite cardIconImage;

    public PoolType effect;
    public Vector2 effectPosition;

    public int randomWeight;
    public int cost = 1;
}
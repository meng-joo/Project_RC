using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CardSO")]
public class CardSO : ScriptableObject
{
    public CardInfo cardInfo;
    
    public Sprite cardIconImage;

    public GameObject effect;
    public Vector2 effectPosition;

    [TextArea]
    public string cardExp;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CardTierListSO")]

public class CardTierListSO : ScriptableObject
{
    public List<CardSO> tierCardList;
}

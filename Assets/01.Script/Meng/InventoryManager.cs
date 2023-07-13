using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<CardSO> inventoryCards;
    public List<CardSO> deckCards;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}

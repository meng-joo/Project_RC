using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<AbCard> inventoryCards;
    public List<AbCard> deckCards;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}

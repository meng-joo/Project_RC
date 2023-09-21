using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InventoryManager : MonoSingleTon<InventoryManager>
{
    public List<CardSO> deckCards;

    private void Awake()
    {
    }
}

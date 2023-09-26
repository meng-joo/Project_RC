using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void SetCard(bool _isOn)
    {
        if (_isOn)
        {
            foreach (var _variable in InventoryManager.Instance.deckCards)
            {
                GameObject _card = PoolManager.Pop(PoolType.VisualCard);
                _card.transform.SetParent(transform);

                _card.GetComponent<CardPool>().SetCardInfo(_variable);
                _card.GetComponentInChildren<AbCard>().SetFontSize(16.2f);
                
                _card.transform.localScale = Vector3.one;
                _card.GetComponentInChildren<AbCard>().PickEffect();
            }
        }
        else
        {
            int _count = transform.childCount;
        
            for (int i = _count - 1; i >= 0; i--)
            {
                transform.GetChild(i).GetComponentInChildren<AbCard>().SetFontSize(12f);
                PoolManager.Push(PoolType.VisualCard, transform.GetChild(i).gameObject);
            }
        }
    }
}

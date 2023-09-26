using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExpUI : MonoBehaviour
{
    private Transform expImage;

    private PoolType poolType;

    private bool _isOn;
    
    private void Start()
    {
        expImage = transform.Find("EXPImage");
        _isOn = false;
        expImage.transform.localScale = Vector3.zero;
    }

    public void PopupCardInfo(CardSO _cardSO)
    {
        if (_isOn) return;
        Sequence _seq = DOTween.Sequence();

        _seq.AppendCallback(FoldPopupCardInfo
        );
        _seq.Append(expImage.DOScale(1, 0.2f));

        _seq.AppendCallback(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                var _card = PoolManager.Pop(PoolType.VisualCard);

                _card.transform.SetParent(expImage);
                _card.GetComponent<CardPool>().SetCardInfo(_cardSO);

                _card.GetComponentInChildren<AbCard>().SetFontSize(16.2f);
                _card.GetComponentInChildren<AbCard>().BreakthroughCard(i, false);

                _card.transform.localScale = Vector3.one;
                _card.GetComponentInChildren<AbCard>().PickEffect(1.5f);
            }
        });
        _isOn = true;
    }

    public void FoldPopupCardInfo()
    {
        if (expImage.childCount <= 0) return;

        int _count = expImage.childCount;
        
        for (int i = _count - 1; i >= 0; i--)
        {
            expImage.GetChild(i).GetComponentInChildren<AbCard>().SetFontSize(12f);
            PoolManager.Push(PoolType.VisualCard, expImage.GetChild(i).gameObject);
        }

        _isOn = false;
        expImage.localScale = Vector3.zero;
    }
}

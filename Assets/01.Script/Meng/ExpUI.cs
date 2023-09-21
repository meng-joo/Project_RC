using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ExpUI : MonoBehaviour
{
    private Transform expImage;

    private PoolType poolType;
    
    private void Start()
    {
        expImage = transform.Find("EXPImage");
        
        expImage.transform.localScale = Vector3.zero;
    }

    public void PopupCardInfo(PoolType _cardType)
    {
        Sequence _seq = DOTween.Sequence();

        _seq.AppendCallback(() =>
            FoldPopupCardInfo(poolType)
        );

        _seq.AppendCallback(() => poolType = _cardType);

        _seq.Append(expImage.DOScale(1, 0.2f));

        _seq.AppendCallback(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                var _card = PoolManager.Pop(_cardType);

                _card.transform.SetParent(expImage);

                _card.GetComponentInChildren<AbCard>().SetFontSize(18f);
                _card.GetComponentInChildren<AbCard>().BreakthroughCard(i, false);

                _card.transform.localScale = Vector3.one;
                _card.GetComponentInChildren<AbCard>().PickEffect(1.5f);
            }
        });
    }

    public void FoldPopupCardInfo(PoolType _cardType)
    {
        if (expImage.childCount <= 0) return;

        int _count = expImage.childCount;
        
        for (int i = _count - 1; i >= 0; i--)
        {
            expImage.GetChild(i).GetComponentInChildren<AbCard>().SetFontSize(12f);
            PoolManager.Push(_cardType, expImage.GetChild(i).gameObject);
        }

        expImage.localScale = Vector3.zero;
    }
}

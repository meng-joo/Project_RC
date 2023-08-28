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

        poolType = _cardType;

        _seq.Append(expImage.DOScale(1, 0.2f));

        _seq.AppendCallback(() =>
        {
            for (int i = 0; i < 3; i++)
            {
                var _card = PoolManager.Pop(_cardType);

                _card.GetComponent<AbCard>().SetFontSize(12f);
                _card.GetComponent<AbCard>().BreakthroughCard(i, false);

                _card.transform.SetParent(expImage);
                _card.transform.DOScale(1.5f, 0.2f);
            }
        });
    }

    public void FoldPopupCardInfo(PoolType _cardType)
    {
        if (expImage.childCount <= 0) return;

        int _count = expImage.childCount;
        
        for (int i = _count - 1; i >= 0; i--)
        {
            expImage.GetChild(i).GetComponent<AbCard>().SetFontSize(18);
            PoolManager.Push(_cardType, expImage.GetChild(i).gameObject);
        }

        expImage.localScale = Vector3.zero;
    }
}

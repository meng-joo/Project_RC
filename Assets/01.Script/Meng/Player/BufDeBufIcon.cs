using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BufType
{
    POISON,
    STRONG,
    NONE
}

public class BufDeBufIcon : PoolAbleObject, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _explainBox;
    
    public BufType bufType;
    
    public Image iconImage;
    public TextMeshProUGUI countText;

    public override void Init_Pop()
    {
        countText ??= GetComponentInChildren<TextMeshProUGUI>();
        iconImage ??= GetComponent<Image>();
    }

    public void SetBuffData(BuffDataSO _buffDataSO, int _count)
    {
        iconImage.sprite = _buffDataSO.icon;
        countText.text = $"{_count}";

        bufType = _buffDataSO.bufType;

        transform.DOShakeRotation(0.8f, 90, 180);
        transform.DOShakePosition(0.8f, 90, 180);
    }
    
    public override void Init_Push()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _explainBox = PoolManager.Pop(PoolType.BuffExplainBox);
        _explainBox.transform.position = Vector3.zero;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PoolManager.Push(PoolType.BuffExplainBox, _explainBox);
    }
}

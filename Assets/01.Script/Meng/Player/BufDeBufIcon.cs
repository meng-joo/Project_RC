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
    WOUND,
    IRONARMOR,
    THORN,
    WEAK,
    NONE
}

public class BufDeBufIcon : PoolAbleObject, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject _explainBox;
    
    public BufType bufType;
    
    public Image iconImage;
    public TextMeshProUGUI countText;
    public string explainText;

    public override void Init_Pop()
    {
        countText ??= GetComponentInChildren<TextMeshProUGUI>();
        iconImage ??= GetComponent<Image>();
    }

    public void SetBuffData(BuffDataSO _buffDataSO, int _count)
    {
        iconImage.sprite = _buffDataSO.icon;
        countText.text = $"{_count}";
        explainText = _buffDataSO.buffExp;

        bufType = _buffDataSO.bufType;

        transform.DOShakeRotation(1f, 20, 180);
        transform.DOShakePosition(1f, 20, 180);
    }

    public void RemoveBuff()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOShakePosition(1f, 15, 180));
        _seq.Join(transform.DOScale(0, 0.8f));

        _seq.AppendCallback(() => PoolManager.Push(PoolType.BuffIcon, gameObject));
    }
    
    public override void Init_Push()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _explainBox = PoolManager.Pop(PoolType.BuffExplainBox);
        _explainBox.GetComponent<BuffExplainBoxUpdate>().SetExplain(explainText);
        
        _explainBox.transform.SetParent(transform.parent.parent);
        _explainBox.transform.localScale = Vector3.one;

        /*_explainBox.transform.position =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x + 150, Input.mousePosition.y - 100, 5));*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PoolManager.Push(PoolType.BuffExplainBox, _explainBox);
    }
}

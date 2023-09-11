using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BufType
{
    POISON,
    STRONG,
    NONE
}

public class BufDeBufIcon : PoolAbleObject
{
    public Image iconImage;
    public TextMeshProUGUI countText;

    public override void Init_Pop()
    {
        countText ??= GetComponentInChildren<TextMeshProUGUI>();
        iconImage ??= GetComponent<Image>();
    }

    public void SetBuffData(BuffDataSO _buffDataSO)
    {
        iconImage.sprite = _buffDataSO.icon;
        countText.text = $"{_buffDataSO.count}";
    }
    
    public override void Init_Push()
    {
        
    }
}

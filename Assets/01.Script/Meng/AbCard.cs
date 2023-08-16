using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AbCard : PoolAbleObject , IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardInfo CardInfo { get; set; }

    public CardSO cardSO;

    private TextMeshProUGUI cardName;
    private Image cardIconImage;
    private Image cardBackImage;
    private Image cardBorderImage;
    private TextMeshProUGUI cardExp;

    private Transform root;
    
    private Camera mainCam;
    
    public override void Init_Pop()
    {
        mainCam = Camera.main;
        SetCardInfo(cardSO);
    }

    public override void Init_Push()
    {
        
    }

    #region Card Setting
    private void SetCashing()
    {
        cardName ??= transform.Find("Name").GetComponent<TextMeshProUGUI>();
        cardIconImage ??= transform.Find("Image").GetComponent<Image>();
        cardBackImage ??= GetComponent<Image>();
        cardBorderImage ??= transform.Find("Border").GetComponent<Image>();
        cardExp ??= transform.Find("Exp").GetComponent<TextMeshProUGUI>();
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        CardInfo = _cardSO.cardInfo;

        SetCashing();


        cardName.text = CardInfo.name;
        cardIconImage.sprite = _cardSO.cardIconImage;
        cardBackImage.color = SetColor(CardInfo.cardType);
        cardBorderImage.color = SetColor(CardInfo.cardTier);

        cardExp.text = _cardSO.cardExp;
    }

    public void SetNextCard()
    {
        
    }

    private Color SetColor(CardType _cardType) => _cardType switch
    {
        CardType.ATK => Color.red,
        CardType.DEF => Color.blue,
        CardType.BUF => Color.green,
        CardType.NONE => Color.white
    };
    private Color SetColor(CardTier _cardTire) => _cardTire switch
    {
        CardTier.R => Color.red,
        CardTier.SR => Color.blue,
        CardTier.SSR => Color.green,
        CardTier.NONE => Color.white
    };
    
    #endregion
    
    #region Card Effect
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        root ??= transform.root;
        root.BroadcastMessage("BeginDrag", transform, SendMessageOptions.DontRequireReceiver);
    }
    public void OnDrag(PointerEventData eventData)
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.position = mainCam.ScreenToWorldPoint(screenPoint);
        root.BroadcastMessage("Drag", transform, SendMessageOptions.DontRequireReceiver);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("EndDrag", transform, SendMessageOptions.DontRequireReceiver);
    }

    #endregion
    
    #region Card Function
    public abstract void CardSkill();
    public abstract void Passive();
    
    #endregion
}
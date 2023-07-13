using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AbCard : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public CardInfo CardInfo { get; set; }

    private Image cardIconImage;
    private Image cardBackImage;
    private Image cardBorderImage;

    private void Start()
    {
        SetCashing();
    }

    #region Card Setting
    private void SetCashing()
    {
        cardIconImage ??= transform.Find("Image").GetComponent<Image>();
        cardBackImage ??= GetComponent<Image>();
        cardBorderImage ??= transform.Find("Border").GetComponent<Image>();
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        CardInfo = _cardSO.cardInfo;

        SetCashing();

        cardIconImage.sprite = _cardSO.cardIconImage;
        cardBackImage.color = SetColor(CardInfo.cardType);
        cardBorderImage.color = SetColor(CardInfo.cardTier);
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

    #endregion
    
    #region Card Function
    public abstract void CardSkill();
    public abstract void Passive();
    
    #endregion
}
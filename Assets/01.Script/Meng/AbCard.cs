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

    private int level = 1;

    public int Level
    {
        get
        {
            return level;
        }
    }

    private TextMeshProUGUI cardName;
    private Image cardIconImage;
    private Image cardBackImage;
    private Image cardBorderImage;
    public Image screenImage;
    private TextMeshProUGUI cardExp;
    private GameObject cardTierEffect;

    private Transform root;
    
    private Camera mainCam;
    protected BattleManager battleManager;
    
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
        screenImage ??= transform.Find("Screen").GetComponent<Image>();

        battleManager ??= FindObjectOfType<BattleManager>();
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        CardInfo = _cardSO.cardInfo;

        level = 1;

        SetCashing();

        SetCardInfo(CardInfo);

        transform.localScale = Vector3.one;
        cardIconImage.sprite = _cardSO.cardIconImage;
        
        screenImage.color = new Color(1, 1, 1, 0);
    }

    public void SetCardInfo(CardInfo _cardInfo)
    {
        cardName.text = _cardInfo.name;
        
        cardBackImage.color = SetColor(_cardInfo.cardType);
        cardBorderImage.color = SetColor(_cardInfo.cardTier);
        if (cardTierEffect == null)
        {
            cardTierEffect = SetTierEffect(_cardInfo.cardTier);
            cardTierEffect.transform.SetParent(transform);
            cardTierEffect.transform.localPosition = Vector3.zero;
            cardTierEffect.transform.localScale = new Vector3(95, 95, 95);
        }
        cardExp.text = _cardInfo.cardExp;
    }

    public void BreakthroughCard(int _upLevel)
    {
        switch (_upLevel)
        {
            case 1:
                switch (level)
                {
                    case 1:
                        SetCardInfo(cardSO.upgradeCardInfo);
                        level = 2;
                        break;
                    case 2:
                        SetCardInfo(cardSO.transcendenceCardInfo);
                        level = 3;
                        break;
                }
                break;
            case 2:
                SetCardInfo(cardSO.transcendenceCardInfo);
                level = 3;
                break;
        }
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

    private GameObject SetTierEffect(CardTier _cardTier) => _cardTier switch
    {
        CardTier.R => PoolManager.Pop(PoolType.REffect),
        CardTier.SR => PoolManager.Pop(PoolType.SREffect),
        CardTier.SSR => PoolManager.Pop(PoolType.SSREffect),
        CardTier.NONE => PoolManager.Pop(PoolType.CEffect)
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
    public abstract float CardSkill();
    public abstract void Passive();
    public abstract void Upgrade();
    public void DiscardCard(params GameObject[] _card)
    {
        foreach (var VARIABLE in _card)
        {
            PoolManager.Push(VARIABLE.GetComponent<AbCard>().CardInfo.cardPoolType, VARIABLE);
            battleManager.arrange.Children.Remove(VARIABLE.transform);
            battleManager.currentSlotCount--;
        }
    }

    #endregion
}
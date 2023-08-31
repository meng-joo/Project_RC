using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public abstract class AbCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
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
    public Image outLineImage;
    private TextMeshProUGUI cardExp;
    private GameObject cardTierEffect;

    private Transform root;
    
    private Camera mainCam;
    private BattleManager battleManager;
    private ExpUI expUI;

    protected BattleManager BattleManager
    {
        get
        {
            battleManager ??= FindObjectOfType<BattleManager>();
            return battleManager;
        }
    }
    
    public void Init_Pop()
    {
        transform.localScale = Vector3.zero;
        mainCam ??= Camera.main;
        SetCardInfo(cardSO);
    }

    public void Init_Push()
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
        outLineImage ??= transform.Find("OutLine").GetComponent<Image>();
        expUI ??= FindObjectOfType<ExpUI>();
    }

    public void SetFontSize(float _size)
    {
        cardExp.fontSize = _size;
        SetCardInfo(CardInfo);
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        CardInfo = _cardSO.cardInfo;

        level = 1;

        SetCashing();

        SetCardInfo(CardInfo);

        cardIconImage.sprite = _cardSO.cardIconImage;
        
        screenImage.color = new Color(1, 1, 1, 0);

        OutLineEffect(false);
    }

    public void SetCardInfo(CardInfo _cardInfo)
    {
        cardName.text = _cardInfo.name;
        
        cardBackImage.color = SetColor(_cardInfo.cardType);
        cardBorderImage.color = SetColor(_cardInfo.cardTier);
        if (cardTierEffect == null)
        {
            if (_cardInfo.cardTier == CardTier.SR || _cardInfo.cardTier == CardTier.SSR)
            {
                cardTierEffect = SetTierEffect(_cardInfo.cardTier);
                cardTierEffect.transform.SetParent(transform);
                cardTierEffect.transform.localPosition = Vector3.zero;
                cardTierEffect.transform.localScale = new Vector3(95, 95, 95);
            }
        }
        cardExp.text = _cardInfo.cardExp;
    }

    public void SetNextCard()
    {
        
    }

    private void UpgradeCard(int _upLevel)
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
                    default:
                        break;
                }

                break;
            case 2:
                SetCardInfo(cardSO.transcendenceCardInfo);
                level = 3;
                break;
            default:
                break;
        }
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
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (BattleManager.IsPlayerTurn)
        {
            transform.localScale = Vector3.one;
            OutLineEffect(false);
            expUI.FoldPopupCardInfo(CardInfo.cardPoolType);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!BattleManager.IsPlayerTurn) return;
        root ??= transform.root;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        root.BroadcastMessage("BeginDrag", transform.parent, SendMessageOptions.DontRequireReceiver);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (BattleManager.IsPlayerTurn)
            {
                OutLineEffect(true);
                expUI.PopupCardInfo(CardInfo.cardPoolType);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.IsPlayerTurn) return;
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.parent.position = mainCam.ScreenToWorldPoint(screenPoint);
        root.BroadcastMessage("Drag", transform.parent, SendMessageOptions.DontRequireReceiver);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (BattleManager.IsPlayerTurn)
        {
            root.BroadcastMessage("EndDrag", transform.parent, SendMessageOptions.DontRequireReceiver);
        }
    }
    
    public float ConsumptionEffect()
    {
        var _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveX(transform.localPosition.x + 170f, 0.5f).SetEase(Ease.InBack));
        _seq.Join(transform.DOScale(0, 0.5f).SetEase(Ease.InBack));
        _seq.Join(screenImage.DOFade(1, 0.1f));

        return _seq.Duration();
    }

    public void OutLineEffect(bool _isOn)
    {
        outLineImage.gameObject.SetActive(_isOn);
    }

    public void PickEffect(float _size = 1, float _time = 0.2f)
    {
        transform.DOScale(_size, _time);
    }

    #endregion
    
    #region Card Function
    public abstract float CardSkill();
    public abstract void Passive();

    public float BreakthroughCard(int _upLevel, bool _isEffectOn = true)
    {
        if (_isEffectOn)
        {
            Sequence _seq = DOTween.Sequence();

            _seq.Append(transform.DOScale(1.2f, 0.4f).SetEase(Ease.InOutBack));
            _seq.Join(screenImage.DOFade(1, 0.3f));
            _seq.AppendCallback(() => UpgradeCard(_upLevel));

            _seq.Append(transform.DOScale(1, 0.8f));
            _seq.Join(screenImage.DOFade(0, 0.2f));

            return _seq.Duration();
        }
        else
        {
            UpgradeCard(_upLevel);
            return 0.5f;
        }
    }

    public void DiscardCard(params GameObject[] _card)
    {
        foreach (var VARIABLE in _card)
        {
            PoolManager.Push(VARIABLE.GetComponentInChildren<AbCard>().CardInfo.cardPoolType, VARIABLE);
            BattleManager.arrange.Children.Remove(VARIABLE.transform);
            BattleManager.currentSlotCount--;
        }
    }

    #endregion
}
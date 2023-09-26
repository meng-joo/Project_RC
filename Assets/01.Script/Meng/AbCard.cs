using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Serialization;

public abstract class AbCard : MonoBehaviour
{
    public CardInfo CardInfo { get; set; }

    public CardSO CardSO
    {
        get
        {
            return cardSO;
        }
        set
        {
            cardSO = value;
            SetCardInfo(value);
        }
    }
    private CardSO cardSO;
    

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
    protected Image screenImage;
    protected Image outLineImage;
    protected Image tierBorder_Lv2;
    protected Image tierBorder_Lv3;
    private TextMeshProUGUI cardExp;
    private GameObject cardTierEffect;

    private CardTier currentCardTierEffect = CardTier.NONE;

    public virtual void InitializationTransform()
    {
        transform.localScale = new Vector3(0, 0.1f, 0);
        transform.localPosition = Vector3.zero;
    }

    public virtual void Init_Push()
    {
        
    }

    private void SetCashing()
    {
        cardName ??= transform.Find("Name").GetComponent<TextMeshProUGUI>();
        cardIconImage ??= transform.Find("Image").GetComponent<Image>();
        cardBackImage ??= GetComponent<Image>();
        cardBorderImage ??= transform.Find("Border").GetComponent<Image>();
        cardExp ??= transform.Find("Exp").GetComponent<TextMeshProUGUI>();
        screenImage ??= transform.Find("Screen").GetComponent<Image>();
        outLineImage ??= transform.Find("OutLine").GetComponent<Image>();
        tierBorder_Lv2 ??= transform.Find("TierBorder_Lv2").GetComponent<Image>();
        tierBorder_Lv3 ??= transform.Find("TierBorder_Lv3").GetComponent<Image>();
    }

    public void SetFontSize(float _size)
    {
        cardExp.fontSize = _size;
    }
    
    public void SetCardInfo(CardSO _cardSO)
    {
        CardInfo = _cardSO.cardInfo;

        level = 1;

        SetCashing();
        
        screenImage.color = Color.white;

        SetCardInfo(CardInfo);

        cardIconImage.sprite = _cardSO.cardIconImage;

        OutLineEffect(false);
    }

    public void SetCardInfo(CardInfo _cardInfo)
    {
        cardName.text = _cardInfo.name;

        cardBackImage.color = SetColor(_cardInfo.cardType);
        cardBorderImage.color = SetColor(_cardInfo.cardTier);
        if (currentCardTierEffect != _cardInfo.cardTier)
        {
            if (cardTierEffect != null)
                PoolManager.Push(cardTierEffect.GetComponent<UIPoolEffect>().PoolType, cardTierEffect);

            if (_cardInfo.cardTier == CardTier.SR || _cardInfo.cardTier == CardTier.SSR)
            {
                cardTierEffect = SetTierEffect(_cardInfo.cardTier);
                cardTierEffect.transform.SetParent(transform);
                cardTierEffect.transform.localPosition = Vector3.zero;
                cardTierEffect.transform.localScale = new Vector3(96, 145, 96);
                ParticleSystemRenderer[] _effects = cardTierEffect.GetComponentsInChildren<ParticleSystemRenderer>();
                int _id = transform.root.GetComponent<Canvas>().sortingLayerID;
                foreach (var _variable in _effects)
                {
                    _variable.sortingLayerID = _id;
                }
            }
        }

        cardExp.text = _cardInfo.cardExp;

        tierBorder_Lv2.gameObject.SetActive(false);
        tierBorder_Lv3.gameObject.SetActive(false);

        if (Level == 2)
            tierBorder_Lv2.gameObject.SetActive(true);
        if (Level == 3)
            tierBorder_Lv3.gameObject.SetActive(true);
    }

    private void UpgradeCard(int _upLevel)
    {
        switch (_upLevel)
        {
            case 1:
                switch (level)
                {
                    case 1:
                        level = 2;
                        SetCardInfo(CardSO.upgradeCardInfo);
                        
                        break;
                    case 2:
                        level = 3;
                        SetCardInfo(CardSO.transcendenceCardInfo);
                        break;
                    default:
                        break;
                }

                break;
            case 2:
                level = 3;
                SetCardInfo(CardSO.transcendenceCardInfo);
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
        CardTier.R => Color.white,
        CardTier.SR => Color.blue,
        CardTier.SSR => Color.red,
        CardTier.NONE => Color.white
    };

    private GameObject SetTierEffect(CardTier _cardTier) => _cardTier switch
    {
        CardTier.R => PoolManager.Pop(PoolType.REffect),
        CardTier.SR => PoolManager.Pop(PoolType.SREffect),
        CardTier.SSR => PoolManager.Pop(PoolType.SSREffect),
        CardTier.NONE => PoolManager.Pop(PoolType.CEffect)
    };

    public void OutLineEffect(bool _isOn)
    {
        outLineImage.gameObject.SetActive(_isOn);
    }

    public void PickEffect(float _size = 1, float _time = 0.6f)
    {
        Sequence _seq = DOTween.Sequence();
        float _divideTime = _time / 10f;

        _seq.Append(transform.DOScaleX(_size, _divideTime * 5));
        _seq.Append(transform.DOScaleY(_size, _divideTime * 3f));
        _seq.Append(screenImage.DOFade(0, _divideTime * 2f));
    }

    public float BreakthroughCard(int _upLevel, bool _isEffectOn = true)
    {
        if (_isEffectOn)
        {
            Sequence _seq = DOTween.Sequence();

           // _seq.Append(transform.DOScale(1.2f, 0.2f).SetEase(Ease.InOutBack));
            _seq.Append(screenImage.DOFade(1, 0.1f));
            _seq.AppendCallback(() => UpgradeCard(_upLevel));

            //_seq.Append(transform.DOScale(1, 0.3f));
            _seq.Append(screenImage.DOFade(0, 0.2f));

            return _seq.Duration();
        }
        else
        {
            UpgradeCard(_upLevel);
            return 0.5f;
        }
    }
}
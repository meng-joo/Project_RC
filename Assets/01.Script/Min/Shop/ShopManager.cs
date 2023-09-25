using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    #region ���� ���� ����
    private readonly int SSRcnt = 1;
    private readonly int SRcnt = 2;
    private readonly int Rcnt = 3;
    #endregion
    public List<ShopCard> shopCardList = new List<ShopCard>();


    public List<CardTier> cardTierList = new List<CardTier>();

    [SerializeField] private GameObject shopCardPrefab = null;
    [SerializeField] private Transform shopCardParentTrm = null;

    [SerializeField] private CardTierListSO SSRTierList;
    [SerializeField] private CardTierListSO SRTierList;
    [SerializeField] private CardTierListSO RTierList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        SettingCardTier();
       CreateCardUI();
        SetCardRandomsInfo();
    }
    public void SettingCardTier()
    {
        for (int i = 0; i < SSRcnt; i++)
        {
            cardTierList[i] = CardTier.SSR;
        }
        for (int i = SSRcnt; i < SRcnt; i++)
        {
            cardTierList[i] = CardTier.SR;
        }
        for (int i = SRcnt; i < Rcnt * 2; i++)
        {
            cardTierList[i] = CardTier.R;
        }

        var shuffle = cardTierList.OrderBy(a => Guid.NewGuid()).ToList();
        cardTierList = shuffle;

    }

    public void CreateCardUI()
    {
        for (int i = 0; i < cardTierList.Count; i++)
        {
            GameObject obj = Instantiate(shopCardPrefab, transform);
            obj.transform.SetParent(shopCardParentTrm);
            obj.transform.localScale = Vector3.one;

            obj.GetComponent<ShopCard>().CardTier = cardTierList[i];
            
            
            shopCardList.Add(obj.GetComponent<ShopCard>());
            
            obj.SetActive((false));
        }

    }


    public void SetCardRandomsInfo()
    {
        for (int i = 0; i < cardTierList.Count; i++)
        {
            switch (shopCardList[i].CardTier)
            {
                case CardTier.R:
                shopCardList[i].CardSO = WeightRandomManger.Instance.WeightRandom(RTierList);
                    break;
                case CardTier.SR:
                    shopCardList[i].CardSO = WeightRandomManger.Instance.WeightRandom(SRTierList);
                    break;
                case CardTier.SSR:
                    shopCardList[i].CardSO = WeightRandomManger.Instance.WeightRandom(SSRTierList);
                    break;
                case CardTier.NONE:
                    break;
                default:
                    break;
            }

            
            
            GameObject obj = PoolManager.Pop(shopCardList[i].CardSO.cardInfo.cardPoolType);
            obj.transform.SetParent(shopCardParentTrm);
            obj.transform.localScale = Vector3.one;
            obj.GetComponentInChildren<AbCard>().PickEffect();
            obj.GetComponentInChildren<AbCard>().SetFontSize(16f);
        }
    }
}

   

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchingCardEventManager : MonoSingleTon<MatchingCardEventManager>
{
    public GameObject cardPrefab;
    public Transform parentTrm;

    public List<CardSO> randomCardList = new List<CardSO>();

    [SerializeField] private CardTierListSO matchingCardList;

    private int matchingCardCnt = 6;
    //[SerializeField] private CardTierListSO SSRTierList;
    //[SerializeField] private CardTierListSO SRTierList;
    //[SerializeField] private CardTierListSO RTierList;

    public List<CardSO> cardMateList = new List<CardSO>();

    public int tryCnt = 5;
    private void Start()
    {
        SetMatchingCardList();
        CreateCard();
    }

    public void SetMatchingCardList()
    {
        for (int i = 0; i < matchingCardCnt; i++)
        {
            randomCardList.Add(WeightRandomManger.Instance.WeightRandom(matchingCardList));   
        }

        for (int i = 0; i < randomCardList.Count; i++)
        {
            cardMateList.Add(randomCardList[i]);
        }

        for (int i = 0; i < cardMateList.Count; i++)
        {
            randomCardList.Add(cardMateList[i]);
        }
        GetShuffleList(randomCardList);
    }

    public void CreateCard()
    {
        foreach (Transform child in parentTrm)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < randomCardList.Count; i++)
        {
            GameObject obj = Instantiate(cardPrefab, transform);
            obj.transform.SetParent(parentTrm);
            obj.GetComponent<MatchCard>().cardSO = randomCardList[i]; 

        }
    }
    public List<CardSO> GetShuffleList(List<CardSO> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i);

            CardSO temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }

        return _list;
    }

}


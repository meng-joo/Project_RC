using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    private Dictionary<string, AbCard> playerCard;
    private Dictionary<string, AbCard> enemyCard;

    public Image deckUI;

    [Space] 
    public TextMeshProUGUI cardPickUpCountText;
    public Image cardPickUpCountGageBar;

    public TextMeshProUGUI activeSlotCountText;

    public UnityEvent turnEndEffect;
    
    [SerializeField] private int activeSlotCount = 3;
    [SerializeField] private int cardSlotCount = 9;
    [SerializeField] public int currentSlotCount = 0;
    [SerializeField] private int cardPickUpCount = 3;
    private int currentCardPickUpCount = 3;
    private int currentActiveSlotCount;
    public int CurrentActiveSlotCount
    {
        get
        {
            activeSlotCountText.text = $"{currentActiveSlotCount}";
            return currentActiveSlotCount;
        }
        set
        {
            currentActiveSlotCount = value;
            activeSlotCountText.text = $"{currentActiveSlotCount}";
        }
    }

    public Arrange arrange;

    private void Start()
    {
        arrange = FindObjectOfType<Arrange>();
        currentCardPickUpCount = cardPickUpCount;
        CurrentActiveSlotCount = activeSlotCount;
    }

    public void ClickPickUpBTN()
    {
        if (currentCardPickUpCount <= 0) return;
        currentCardPickUpCount--;
        PickUpCard();
        UpdatePickUpCountUI();
    }

    public void PickUpCard()
    {
        if (cardSlotCount <= currentSlotCount) return;
        currentSlotCount++;
            
        //여기에 가중치 랜덤 들어가야 합니다
        var a = Random.Range(0, InventoryManager.instance.deckCards.Count);
        var _card = PoolManager.Pop(InventoryManager.instance.deckCards[a].cardInfo.cardPoolType);
        _card.transform.SetParent(deckUI.transform);
            
        arrange.UpdateChildren();
    }

    public void ClickTurnEndBTN()
    {
        StartCoroutine(UsePlayerCard("Player"));
    }

    public void TurnEnd(string _name)
    {
        
    }

    IEnumerator UsePlayerCard(string _name)
    {
        int _activeCount = CurrentActiveSlotCount >= currentSlotCount ? currentSlotCount : CurrentActiveSlotCount;

        yield return new WaitForSecondsRealtime(0.3f);
        
        for (int i = 0; i < _activeCount; i++)
        {
            if (currentSlotCount < 0) yield return null;
            float _delay = arrange.Children[0].GetComponent<AbCard>().CardSkill();

            yield return new WaitForSecondsRealtime(_delay);
        }
        
        yield return new WaitForSeconds(1f);

        TurnEnd(_name);
    }

    private void UpdatePickUpCountUI()
    {
        cardPickUpCountText.text = $"{currentCardPickUpCount}/{cardPickUpCount}";

        cardPickUpCountGageBar.fillAmount = (float)currentCardPickUpCount / cardPickUpCount;
    }
}

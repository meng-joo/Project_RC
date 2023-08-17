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

    public UnityEvent turnEndEffect;
    
    [SerializeField] private int activeSlotCount = 3;
    [SerializeField] private int cardSlotCount = 9;
    [SerializeField] public int currentSlotCount = 0;
    [SerializeField] private int cardPickUpCount = 3;
    private int currentCardPickUpCount = 3;

    public Arrange arrange;

    private void Start()
    {
        arrange = FindObjectOfType<Arrange>();
        currentCardPickUpCount = cardPickUpCount;
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
        StartCoroutine(UsePlayerCard());
    }

    public void TurnEnd()
    {
        
    }

    IEnumerator UsePlayerCard()
    {
        int _activeCount = activeSlotCount >= currentSlotCount ? currentSlotCount : activeSlotCount;
        
        for (int i = 0; i < _activeCount; i++)
        {
            if (currentSlotCount < 0) yield return null;
            float _delay = arrange.Children[0].GetComponent<AbCard>().CardSkill();

            yield return new WaitForSeconds(_delay);
        }
        
        yield return new WaitForSeconds(1f);
    }

    private void UpdatePickUpCountUI()
    {
        cardPickUpCountText.text = $"{currentCardPickUpCount}/{cardPickUpCount}";

        cardPickUpCountGageBar.fillAmount = (float)currentCardPickUpCount / cardPickUpCount;
    }
}

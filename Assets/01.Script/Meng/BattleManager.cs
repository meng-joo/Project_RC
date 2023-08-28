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
    private List<AbCard> activeSlot = new List<AbCard>();

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
        UpdatePickUpCountUI();
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
        //현재 풀에있는 카드 수와 전개 카드 수 비교하고 최소값 비교
        int _activeCount = Mathf.Min(CurrentActiveSlotCount, currentSlotCount);

        yield return new WaitForSecondsRealtime(0.3f);

        //지금 보고 있는 카드 번호 (0부터)
        int cardNum = 0;

        //현재 풀에있는 카드 개수
        int cardCount = currentSlotCount;
        
        //카드 만큼 돌기
        for (int i = 0; i < cardCount; i++)
        {
            //현재 카드번호와 전개 카드 수 비교
            if (cardNum <= _activeCount - 2)
            {
                //혹시 현재 카드가 3레벨인가? 그럼 건너뛰기
                if (arrange.Children[cardNum].GetComponent<AbCard>().Level >= 3 || arrange.Children.Count <= 1)
                {
                    cardNum++;
                    continue;
                }
                //현재카드와 다음카드가 같은 종류라면...? 뒤에카드 업그레이드 후 현재카드 지우기
                if (arrange.Children[cardNum].GetComponent<AbCard>().CardInfo.cardPoolType == arrange.Children[cardNum + 1].GetComponent<AbCard>().CardInfo.cardPoolType)
                {
                    arrange.Children[cardNum + 1].GetComponent<AbCard>()
                        .BreakthroughCard(arrange.Children[cardNum].GetComponent<AbCard>().Level);
                    
                    arrange.Children[cardNum].GetComponent<AbCard>().DiscardCard(arrange.Children[cardNum].gameObject);
                }
                //아니면 다음카드 보기
                else
                {
                    cardNum++;
                }
            }
        }

        foreach (var VARIABLE in arrange.Children)
        {
            activeSlot.Add(VARIABLE.GetComponent<AbCard>());
        }

        foreach (var VARIABLE in activeSlot)
        {
            float _delay = VARIABLE.CardSkill();
            yield return new WaitForSecondsRealtime(_delay);
        }

        yield return new WaitForSeconds(1f);

        activeSlot.Clear();
        TurnEnd(_name);
    }

    private void UpdatePickUpCountUI()
    {
        cardPickUpCountText.text = $"{currentCardPickUpCount}/{cardPickUpCount}";

        cardPickUpCountGageBar.fillAmount = (float)currentCardPickUpCount / cardPickUpCount;
    }
}

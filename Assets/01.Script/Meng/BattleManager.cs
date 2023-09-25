using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemy;

    public Player Player
    {
        get
        {
            player ??= FindObjectOfType<Player>();
            return player;
        }
        set => player = value;
    }
    public Enemy Enemy
    {
        get
        {
            enemy ??= FindObjectOfType<Enemy>();
            return enemy;
        }
        set => enemy = value;
    }

    [SerializeField] private bool isPlayerTurn;
    
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

    public bool IsPlayerTurn
    {
        get => isPlayerTurn;
        private set => isPlayerTurn = value;
    }

    public Arrange arrange;
    public TurnChangeEffect turnChangeEffect;

    private void Start()
    {
        arrange = FindObjectOfType<Arrange>();
        currentCardPickUpCount = cardPickUpCount;
        CurrentActiveSlotCount = activeSlotCount;
        UpdatePickUpCountUI();
        IsPlayerTurn = false;

        StartCoroutine(PlayerTurn());
    }

    public void ClickPickUpBTN()
    {
        if (currentCardPickUpCount <= 0) return;
        if (!IsPlayerTurn) return;
        currentCardPickUpCount--;
        PickUpCard();
        UpdatePickUpCountUI();
    }

    public void PickUpCard()
    {
        if (cardSlotCount <= currentSlotCount) return;
        currentSlotCount++;
            
        //여기에 가중치 랜덤 들어가야 합니다
        var a = Random.Range(0, InventoryManager.Instance.deckCards.Count);
        var _card = PoolManager.Pop(InventoryManager.Instance.deckCards[a].cardInfo.cardPoolType);
        _card.transform.SetParent(deckUI.transform);
        _card.transform.localScale = Vector3.one;
        _card.GetComponentInChildren<AbCard>().PickEffect();
        _card.GetComponentInChildren<AbCard>().SetFontSize(13f);

        arrange.UpdateChildren();
    }

    public void ClickTurnEndBTN()
    {
        if (!IsPlayerTurn) return;
        IsPlayerTurn = false;
        StartCoroutine(UsePlayerCard());
    }

    public void TurnEnd(string _name)
    {
        switch (_name)
        {
            case "Player":
                StartCoroutine(PlayerTurn());
                break;
            case "Enemy":
                StartCoroutine(EnemyTurn());
                break;
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(turnChangeEffect.ChangingEffect("적", "턴"));

        yield return new WaitForSeconds(Enemy.MyTurnStart());

        yield return new WaitForSeconds(1.2f);
        
        yield return new WaitForSeconds(Enemy.Skill());
        
        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(Enemy.MyTurnEnd());
        
        TurnEnd("Player");
    }

    IEnumerator PlayerTurn()
    {
        yield return new WaitForSeconds(turnChangeEffect.ChangingEffect("내", "턴"));

        yield return new WaitForSeconds(Player.MyTurnStart());
        Player.ShieldCount = 0;
        
        IsPlayerTurn = true;
        CurrentActiveSlotCount = activeSlotCount;
        currentCardPickUpCount = cardPickUpCount;
        UpdatePickUpCountUI();
    }
    
    IEnumerator UsePlayerCard()
    {
        //현재 풀에있는 카드 수와 전개 카드 수 비교하고 최소값 비교
        int _activeCount = Mathf.Min(CurrentActiveSlotCount, currentSlotCount);

        yield return new WaitForSeconds(turnChangeEffect.ChangingEffect("전", "개"));

        //지금 보고 있는 카드 번호 (0부터)
        int cardNum = 0;

        //현재 풀에있는 카드 개수
        int cardCount = currentSlotCount;
        
        //카드 만큼 돌기
        for (int i = 0; i < cardCount; i++)
        {
            //현재 카드번호와 전개 카드 수 비교
            if (cardNum <= arrange.Children.Count - 2)
            {
                //혹시 현재 카드가 3레벨인가? 그럼 건너뛰기
                if (arrange.Children[cardNum].GetComponentInChildren<AbCard>().Level >= 3 || arrange.Children.Count <= 1)
                {
                    cardNum++;
                    continue;
                }
                //현재카드와 다음카드가 같은 종류라면...? 뒤에카드 업그레이드 후 현재카드 지우기
                if (arrange.Children[cardNum].GetComponentInChildren<AbCard>().CardInfo.cardPoolType == arrange.Children[cardNum + 1].GetComponentInChildren<AbCard>().CardInfo.cardPoolType)
                {
                   yield return new WaitForSeconds(arrange.Children[cardNum].GetComponentInChildren<AbCard>().ConsumptionEffect());
                   yield return new WaitForSeconds(arrange.Children[cardNum + 1].GetComponentInChildren<AbCard>()
                       .BreakthroughCard(arrange.Children[cardNum].GetComponentInChildren<AbCard>().Level));
                   
                   arrange.Children[cardNum].GetComponentInChildren<AbCard>().DiscardCard(arrange.Children[cardNum].gameObject);
                    yield return new WaitForSeconds(0.3f);
                    //_activeCount;
                }
                //아니면 다음카드 보기
                else
                {
                    cardNum++;
                }
            }
        }

        int activeCount = 0;
        foreach (var VARIABLE in arrange.Children)
        {
            if (activeCount >= _activeCount) break;
            activeSlot.Add(VARIABLE.GetComponentInChildren<AbCard>());
            activeCount++;
        }

        foreach (var VARIABLE in activeSlot)
        {
            float _delay = VARIABLE.CardSkill();
            yield return new WaitForSeconds(_delay);
        }

        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(Player.MyTurnEnd());

        activeSlot.Clear();
        TurnEnd("Enemy");
    }

    private void UpdatePickUpCountUI()
    {
        cardPickUpCountText.text = $"{currentCardPickUpCount}/{cardPickUpCount}";

        cardPickUpCountGageBar.fillAmount = (float)currentCardPickUpCount / cardPickUpCount;
    }
}

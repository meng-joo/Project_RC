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
using Sequence = DG.Tweening.Sequence;

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
    [SerializeField] private bool isBattle;
    
    private List<BattleCardBase> activeSlot = new List<BattleCardBase>();

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

    public void StartBattle()
    {
        Sequence _seq = DOTween.Sequence();
        arrange = FindObjectOfType<Arrange>();
        currentCardPickUpCount = cardPickUpCount;
        CurrentActiveSlotCount = activeSlotCount;
        IsPlayerTurn = false;
        isBattle = true;

        _seq.AppendCallback(() =>
        {
            Player.transform.position = new Vector3(-10, Player.transform.position.y, Player.transform.position.z);
            Enemy.transform.position = new Vector3(10, Enemy.transform.position.y, Enemy.transform.position.z);

            Player.Run();
            Enemy.Run();
        });

        _seq.Append(Player.transform.DOLocalMoveX(-6, 3));
        _seq.Join(Enemy.transform.DOLocalMoveX(6, 3));

        _seq.AppendCallback(() =>
        {
            Player.Idle();
            Enemy.Idle();
        });

        _seq.AppendInterval(0.5f);
        _seq.AppendCallback(() =>
        {
            Player.SetUnitCanvas();
            Enemy.SetUnitCanvas();
        });
        _seq.AppendInterval(0.5f);
        
        _seq.AppendCallback(() =>
        {
            UpdatePickUpCountUI();
            StartCoroutine(PlayerTurn());
        });
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
        _card.GetComponent<CardPool>().SetCardInfo(InventoryManager.Instance.deckCards[a]);
        _card.GetComponentInChildren<BattleCardBase>().PickEffect();
        _card.GetComponentInChildren<BattleCardBase>().SetFontSize(15f);

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
        Enemy.ShieldCount = 0;

        yield return new WaitForSeconds(Enemy.MyTurnStart());
        if (!isBattle) yield break;

        yield return new WaitForSeconds(0.6f);
        
        yield return new WaitForSeconds(Enemy.Skill());
        if (!isBattle) yield break;
        
        yield return new WaitForSeconds(0.6f);
        
        yield return new WaitForSeconds(Enemy.MyTurnEnd());
        if (!isBattle) yield break;
        
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
                if (arrange.Children[cardNum].GetComponentInChildren<BattleCardBase>().Level >= 3 ||
                    arrange.Children.Count <= 1 ||
                    arrange.Children[cardNum + 1].GetComponentInChildren<BattleCardBase>().Level >= 3)
                {
                    cardNum++;
                    continue;
                }

                //현재카드와 다음카드가 같은 종류라면...? 뒤에카드 업그레이드 후 현재카드 지우기
                if (arrange.Children[cardNum].GetComponentInChildren<BattleCardBase>().CardInfo.cardPoolType == arrange.Children[cardNum + 1].GetComponentInChildren<BattleCardBase>().CardInfo.cardPoolType)
                {
                   yield return new WaitForSeconds(arrange.Children[cardNum].GetComponentInChildren<BattleCardBase>().ConsumptionEffect() - 0.1f);
                   yield return new WaitForSeconds(arrange.Children[cardNum + 1].GetComponentInChildren<BattleCardBase>()
                       .BreakthroughCard(arrange.Children[cardNum].GetComponentInChildren<BattleCardBase>().Level));
                   
                   arrange.Children[cardNum].GetComponentInChildren<BattleCardBase>().DiscardCard(arrange.Children[cardNum].gameObject);
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
            activeSlot.Add(VARIABLE.GetComponentInChildren<BattleCardBase>());
            activeCount++;
        }

        foreach (var VARIABLE in activeSlot)
        {
            float _delay = VARIABLE.CardSkill();
            if (!isBattle) yield break;
            yield return new WaitForSeconds(_delay);
        }

        yield return new WaitForSeconds(0.6f);
        
        yield return new WaitForSeconds(Player.MyTurnEnd());
        if (!isBattle) yield break;

        activeSlot.Clear();
        TurnEnd("Enemy");
    }

    private void UpdatePickUpCountUI()
    {
        cardPickUpCountText.text = $"{currentCardPickUpCount}/{cardPickUpCount}";

        cardPickUpCountGageBar.fillAmount = (float)currentCardPickUpCount / cardPickUpCount;
    }

    public void EndBattle()
    {
        IsPlayerTurn = false;
        isBattle = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            Enemy.Hit(1000);
    }
}

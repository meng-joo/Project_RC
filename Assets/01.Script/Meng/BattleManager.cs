using System;
using System.Collections;
using System.Collections.Generic;
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

    public UnityEvent turnEndEffect;
    
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int activeSlotCount = 3;

    private Arrange arrange;

    private void Start()
    {
        arrange = FindObjectOfType<Arrange>();
    }

    public void PickUpCard()
    {
        var a = Random.Range(0, InventoryManager.instance.deckCards.Count);

        //var _card = Instantiate(cardPrefab);
        //_card.GetComponent<AbCard>().SetCardInfo(InventoryManager.instance.deckCards[a]);

        var _card = PoolManager.Pop(InventoryManager.instance.deckCards[a].cardInfo.cardPoolType);
        _card.transform.SetParent(deckUI.transform);
        //canvasCentral.
        
        arrange.UpdateChildren();
        
    }
    
    private void TurnEnd()
    {
        
    }

    IEnumerator UsePlayerCard()
    {
        yield return new WaitForSeconds(1f);
        
    }
}

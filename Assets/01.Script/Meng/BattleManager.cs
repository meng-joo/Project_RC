using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    private Dictionary<string, AbCard> playerCard;
    private Dictionary<string, AbCard> enemyCard;

    public UnityEvent turnEndEffect;
    
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int activeSlotCount = 3;

    private void PickUpCard()
    {
        var a = Random.Range(0, InventoryManager.instance.deckCards.Count);

        var _card = Instantiate(cardPrefab);
        _card.GetComponent<AbCard>().SetCardInfo(InventoryManager.instance.deckCards[a]);
    }
    
    private void TurnEnd()
    {
        
    }

    IEnumerator UsePlayerCard()
    {
        yield return new WaitForSeconds(1f);
        
    }
}

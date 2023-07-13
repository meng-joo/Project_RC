using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleManager : MonoBehaviour
{
    private Dictionary<string, AbCard> playerCard;
    private Dictionary<string, AbCard> enemyCard;

    public UnityEvent turnEndEffect;

    private int activeSlotCount = 3;


    private void PickUpCard()
    {
        int a = Random.Range(0, InventoryManager.instance.deckCards.Count + 1);

        
    }
    
    private void TurnEnd()
    {
        
    }

    IEnumerator UsePlayerCard()
    {
        yield return new WaitForSeconds(1f);
        
    }
}

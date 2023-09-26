using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class Click
{
    public static CardSO clickCardSO; //현재클릭한걸알수있어
    public static bool isSelected;
    public static MatchCard clickMatchCard;
}

public class MatchCard : MonoBehaviour
{
    public CardSO cardSO;
    public Button btn;
    public bool isClick = false;


    private void Start()
    {
        btn = GetComponent<Button>();
       AddEventAction(this, EventTriggerType.PointerClick, (data) => { OnClick(this, (PointerEventData)data); });
    }
    
    protected void AddEventAction(MatchCard matchCard, EventTriggerType eventTriggerType, UnityAction<BaseEventData> BaseEventDataAction)
    {
        EventTrigger eventTrigger = matchCard.GetComponent<EventTrigger>();

        if (!eventTrigger)
        {
            Debug.LogWarning("Nothing Events!");
            return;
        }

        EventTrigger.Entry eventTriggerEntry = new EventTrigger.Entry { eventID = eventTriggerType };
        eventTriggerEntry.callback.AddListener(BaseEventDataAction);
        eventTrigger.triggers.Add(eventTriggerEntry);
    }

    public void OnClick(MatchCard matchCard, PointerEventData data)
    {

        if (MatchingCardEventManager.Instance.tryCnt <= 0)
        {
            Debug.Log("이벤트끝남");
        }

        if (Click.isSelected)
        {

            if (Click.clickMatchCard == matchCard)
            {
                Debug.Log("이게 카드맞추기인데 같은걸누르면ㅇ ㅓ카니 ㅄ아");
                Click.isSelected = !Click.isSelected;
                return;
            }

            MatchingCardEventManager.Instance.tryCnt--;

            if (Click.clickCardSO == matchCard.cardSO)
            {
                isClick = false;
                Debug.Log("같은카드 카드를 얻으셨습니다");
            }
            else
            {
                isClick = false;
                Debug.Log("틀리셨습니다");
            }
        }
        else
        {
            Click.clickCardSO = matchCard.cardSO;
            Click.clickMatchCard = matchCard;
        }


        isClick = !isClick;
        Debug.Log("isCLick:     "+isClick);
        Click.isSelected = !Click.isSelected;
    }
}

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
    public static MatchCard clickCard; //현재클릭한걸알수있어
    public static bool isSelected;
}

public class MatchCard : MonoBehaviour
{
    public CardSO cardSO;
    public Button btn;

    private void Start()
    {
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
        if (Click.isSelected)
        {
            #region 데이터 스왑
            CardSO cardTemp = this.cardSO;
            this.cardSO = Click.clickCard.cardSO;
            Click.clickCard.cardSO = cardTemp;
            #endregion

            MatchingCardEventManager.Instance.tryCnt--;

            if (cardSO == matchCard.cardSO)
            {

                Debug.Log("같은카드 카드를 얻으셨습니다");
            }
            else
            {
                Debug.Log("틀리셨습니다 대가리가 빡이시군요");
            }
        }
    }
}

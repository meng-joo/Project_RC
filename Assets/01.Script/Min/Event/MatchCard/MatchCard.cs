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
    public static CardSO clickCardSO; //����Ŭ���Ѱɾ˼��־�
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
            Debug.Log("�̺�Ʈ����");
        }

        if (Click.isSelected)
        {

            if (Click.clickMatchCard == matchCard)
            {
                Debug.Log("�̰� ī����߱��ε� �����ɴ����餷 ��ī�� ����");
                Click.isSelected = !Click.isSelected;
                return;
            }

            MatchingCardEventManager.Instance.tryCnt--;

            if (Click.clickCardSO == matchCard.cardSO)
            {
                isClick = false;
                Debug.Log("����ī�� ī�带 �����̽��ϴ�");
            }
            else
            {
                isClick = false;
                Debug.Log("Ʋ���̽��ϴ�");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventRoomType
{
    MatchingCards, //(ī����߱�)
    Sacrifice, //���� �ְ� (ī�������)
    Roulette, //�귿
}

[CreateAssetMenu(menuName = "SO/EventType")]
public class EventTypeSO : ScriptableObject
{
}

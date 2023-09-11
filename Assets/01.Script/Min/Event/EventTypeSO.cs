using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventRoomType
{
    MatchingCards, //(카드맞추기)
    Sacrifice, //제물 주고 (카드지우기)
    Roulette, //룰렛
}

[CreateAssetMenu(menuName = "SO/EventType")]
public class EventTypeSO : ScriptableObject
{
}

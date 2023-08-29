using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventRoomType
{
    DeleteCardRoom,
    RestRoom,
    GoldRoom,
    PicKCardRoom,
}

[CreateAssetMenu(menuName = "SO/EventType")]
public class EventTypeSO : ScriptableObject
{
}

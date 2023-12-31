using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapType
{
    Battle,
    Event,
    Shop,
    Boss
}


[CreateAssetMenu(menuName = "SO/MapType")]
public class MapSO: ScriptableObject
{
    public Sprite mapSprite;

    public MapType mapType;
}

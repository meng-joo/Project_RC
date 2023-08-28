using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapConfig")]
public class MapConfigSO : ScriptableObject
{
    public List<MapSO> maps;
}

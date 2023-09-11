using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapStageSetting")]
public class MapStageSettingSO : ScriptableObject
{
    public int garoCnt;
    public int seroCnt;

    public int rowNodeMinCnt;
    public int rowNodeMaxCnt;

    public int nodeActiveMinCnt;
    public int nodeActiveMaxCnt;

    [Range (5, 100)]
    public int mapCnt;


    [Range(1, 100)]
    public int battleMapCnt;

    [Range(1, 100)]
    public int eventMapCnt;

    [Range(1, 100)]
    public int shopMapCnt;
}

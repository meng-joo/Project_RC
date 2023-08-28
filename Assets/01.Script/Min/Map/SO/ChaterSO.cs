using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Chater")]
public class ChaterSO : ScriptableObject
{
    [Range (5,10)]
    public int mapCnt;


    [Range(1, 10)]
    public int battleMapCnt;

    [Range(1, 10)]
    public int eventMapCnt;

    [Range(1, 10)]
    public int shopMapCnt;
}

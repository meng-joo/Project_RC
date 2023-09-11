using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoSingleTon<BuffManager>
{
    public void AddBuff(Unit _unit, BuffDataSO _buffDataSO)
    {
        foreach (var VARIABLE in _unit.buffDataList)
        {
            if (VARIABLE.bufType == _buffDataSO.bufType)
            {
                
            }
        }
    }
}

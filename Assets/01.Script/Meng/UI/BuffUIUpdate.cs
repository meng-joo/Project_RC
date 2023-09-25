using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffUIUpdate : MonoBehaviour
{
    private Dictionary<BufType, BufDeBufIcon> bufTypeList = new Dictionary<BufType, BufDeBufIcon>();

    public void UpdateBuffUI(BuffDataSO _buffDataSO, int _count)
    {
        if (!bufTypeList.ContainsKey(_buffDataSO.bufType))
        {
            if (_count > 0)
            {
                var _icon = PoolManager.Pop(PoolType.BuffIcon);
                _icon.transform.SetParent(transform);
                _icon.transform.localScale = Vector3.one;
                _icon.GetComponent<BufDeBufIcon>().SetBuffData(_buffDataSO, _count);
                bufTypeList.Add(_buffDataSO.bufType, _icon.GetComponent<BufDeBufIcon>());
            }
            else
            {
                RemoveBuffUI(_buffDataSO.bufType);
            }
        }

        else
        {
            bufTypeList[_buffDataSO.bufType].SetBuffData(_buffDataSO, _count);
        }
    }

    public void RemoveBuffUI(BufType _bufType)
    {
        bufTypeList[_bufType].RemoveBuff();
        bufTypeList.Remove(_bufType);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffUIUpdate : MonoBehaviour
{
    private Dictionary<BuffDataSO, BufDeBufIcon> bufTypeList = new Dictionary<BuffDataSO, BufDeBufIcon>();

    public void UpdateBuffUI(BuffDataSO _buffDataSO, int _count, bool _isNew)
    {
        if (_isNew)
        {
            var _icon = PoolManager.Pop(PoolType.BuffIcon);
            _icon.transform.SetParent(transform);
            _icon.transform.localScale = Vector3.one;
            _icon.GetComponent<BufDeBufIcon>().SetBuffData(_buffDataSO, _count);
            bufTypeList.Add(_buffDataSO, _icon.GetComponent<BufDeBufIcon>());
        }

        else
        {
            bufTypeList[_buffDataSO].SetBuffData(_buffDataSO, _count);
        }
    }
}
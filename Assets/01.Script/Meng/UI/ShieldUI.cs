using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    private TextMeshProUGUI shieldCount;
    private void Start()
    {
        shieldCount = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateShieldCountUI(int _count)
    {
        if (_count > 0)
        {
            gameObject.SetActive(true);
            shieldCount.transform.DOScale(1.2f, 0.15f).OnComplete(() => shieldCount.transform.DOScale(1f, 0.4f));
            shieldCount.text = _count.ToString();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

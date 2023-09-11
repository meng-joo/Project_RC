using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpEffect : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpEffectBar;
    [SerializeField] private TextMeshProUGUI hpText;

    private int beforeHp;

    public void SetHPBar(int _maxHp, int _currentHp)
    {
        hpText.text = $"{_currentHp}/{_maxHp}";

        float _before = hpBar.fillAmount;
        DOTween.To(() => _before, x => hpBar.fillAmount = x, (float)_currentHp / _maxHp, 0.9f);
        
        beforeHp = _currentHp;
    }
}

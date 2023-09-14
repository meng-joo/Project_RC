using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static void CreateDamageText(Vector3 _pos, int _damage, Color _color)
    {
        var _damageText = PoolManager.Pop(PoolType.DamageText);

        _damageText.GetComponent<TextMeshPro>().text = _damage.ToString();
        _damageText.GetComponent<TextMeshPro>().fontSize = Mathf.Clamp(8, _damage / 3, 23);
        _damageText.transform.localPosition = new Vector3(_pos.x + Random.Range(-.5f, .5f), 2.15f);

        _damageText.GetComponent<TextMeshPro>().color = _color;
        _damageText.GetComponent<TextMeshPro>().DOColor(Color.white, 0.1f);

        _damageText.transform.DOLocalMoveY(3.7f, 1f);
        _damageText.transform.DOScale(0, 1);
    }
}

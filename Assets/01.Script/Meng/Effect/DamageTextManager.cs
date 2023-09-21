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
        _damageText.GetComponent<TextMeshPro>().fontSize = Mathf.Clamp(_damage / 3, 8, 27);
        _damageText.transform.position = new Vector3(_pos.x + Random.Range(-1f, 1f), _pos.y + 1.8f);

        _damageText.GetComponent<TextMeshPro>().color = _color;
        _damageText.GetComponent<TextMeshPro>().DOColor(Color.white, 0.8f);

        _damageText.transform.DOLocalMoveY(3.7f, 1f);
        _damageText.transform.DOScale(0.4f, 1);
    }
}

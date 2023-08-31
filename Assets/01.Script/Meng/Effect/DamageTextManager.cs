using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public static void CreateDamageText(int _damage, Color _color)
    {
        var _damageText = PoolManager.Pop(PoolType.DamageText);

        _damageText.GetComponent<TextMeshPro>().text = _damage.ToString();
        _damageText.transform.localPosition = new Vector3(Random.Range(5.5f, 6.5f), 2.15f);

        _damageText.GetComponent<TextMeshPro>().color = _color;
        _damageText.GetComponent<TextMeshPro>().DOColor(Color.white, 0.1f);

        _damageText.transform.DOLocalMoveY(3.7f, 1f);
        _damageText.transform.DOScale(0, 1);
    }
}

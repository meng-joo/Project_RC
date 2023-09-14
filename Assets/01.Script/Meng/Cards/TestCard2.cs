using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestCard2 : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Insert(0f, screenImage.DOFade(1, 1f));
        _seq.AppendCallback(AttackEnemy);
        _seq.Append(screenImage.DOFade(0, 0.2f));

        return _seq.Duration() + 0.2f;
    }

    public override void Passive()
    {
        
    }

    private void AttackEnemy()
    {
        FindObjectOfType<Player>().Attack(0);
        FindObjectOfType<Enemy>().Hit(0);
        
        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;

        DamageTextManager.CreateDamageText(FindObjectOfType<Enemy>().transform.position, 0, Color.red);
        
        BattleManager.CurrentActiveSlotCount--;
    }
}

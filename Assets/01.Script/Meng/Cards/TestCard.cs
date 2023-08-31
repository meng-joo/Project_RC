using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestCard : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 1f));
        _seq.Insert(0f,screenImage.DOFade(1, 1f));

        _seq.Append(transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
        
        _seq.AppendCallback(AttackEnemy);

        return 2;
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

        DamageTextManager.CreateDamageText(0, Color.red);
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestCard : AbCard
{
    [SerializeField] private BuffDataSO Poison;
    
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
        FindObjectOfType<Player>().Attack(200);
        //FindObjectOfType<Enemy>().Hit(200);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;

        var _effect2 = PoolManager.Pop(PoolType.BloodEffect_1);
        _effect2.transform.position = cardSO.effectPosition;
        
        FindObjectOfType<Enemy>().AddBuff(Poison, 1);

        DamageTextManager.CreateDamageText(FindObjectOfType<Enemy>().transform.position,200, Color.red);
        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

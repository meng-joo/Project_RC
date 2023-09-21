using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Attack_Punch : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 0.6f));
        _seq.Insert(0f,screenImage.DOFade(1, .6f));

        _seq.Append(transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
        
        _seq.AppendCallback(AttackEnemy);

        return _seq.Duration() + 0.2f;
    }

    public override void Passive()
    {
        
    }
    
    private void AttackEnemy()
    {
        int damage = 0;
        switch (Level)
        {
            case 1:
                damage = 10;
                break;
            case 2:
                damage = 15;
                break;
            case 3:
                damage = 23;
                break;
            default:
                damage = 0;
                break;
        }

        FindObjectOfType<Player>().Attack(damage);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;
        
        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

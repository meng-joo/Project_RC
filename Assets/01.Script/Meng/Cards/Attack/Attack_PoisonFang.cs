using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack_PoisonFang : AbCard
{
    [SerializeField] private BuffDataSO poison;
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 0.6f));
        _seq.Insert(0f,screenImage.DOFade(1, 0.6f));

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
        int _poison = 0;

        switch (Level)
        {
            case 1:
                damage = 10;
                _poison = 5;
                break;
            case 2:
                damage = 14;
                _poison = 10;
                break;
            case 3:
                damage = 19;
                _poison = 20;
                break;
            default:
                damage = 0;
                break;
        }

        FindObjectOfType<Player>().Attack(damage);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;

        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);

        FindObjectOfType<Enemy>().AddBuff(poison, _poison);

        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

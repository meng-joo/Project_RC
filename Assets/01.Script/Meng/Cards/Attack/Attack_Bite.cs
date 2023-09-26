using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack_Bite : BattleCardBase
{
    [SerializeField] private BuffDataSO _wound;
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
        int _damage = 0;
        int _heal = 0;

        switch (Level)
        {
            case 1:
                _damage = 8;
                _heal = 8;
                break;
            case 2:
                _damage = 10;
                _heal = 10;
                break;
            case 3:
                _damage = 20;
                _heal = 15;
                break;
            default:
                _damage = 0;
                break;
        }

        FindObjectOfType<Player>().Attack(_damage);
        FindObjectOfType<Player>().Heal(_heal, false);

        var _effect = PoolManager.Pop(CardSO.effect);
        _effect.transform.position = CardSO.effectPosition;

        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
        if (Level >= 3)
            FindObjectOfType<Enemy>().AddBuff(_wound, 2);

        BattleManager.CurrentActiveSlotCount -= CardSO.cost;
        DiscardCard(transform.parent.gameObject);
    }
}

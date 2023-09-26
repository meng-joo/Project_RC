using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack_Scratch : BattleCardBase
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
        int _woundCount = 0;

        switch (Level)
        {
            case 1:
                _damage = 5;
                _woundCount = 2;
                break;
            case 2:
                _damage = 10;
                _woundCount = 8;
                break;
            case 3:
                _damage = 23;
                _woundCount = 19;
                break;
            default:
                _damage = 0;
                break;
        }

        FindObjectOfType<Player>().Attack(_damage);

        var _effect = PoolManager.Pop(CardSO.effect);
        _effect.transform.position = CardSO.effectPosition;

        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
        FindObjectOfType<Enemy>().AddBuff(_wound, _woundCount);

        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

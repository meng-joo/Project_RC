using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Attack_ShieldCharge : AbCard
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
        float _weight = 0;
        int _shieldCount = 0;
        switch (Level)
        {
            case 1:
                _shieldCount = 2;
                _weight = 0.5f;
                break;
            case 2:
                _shieldCount = 5;
                _weight = 1;
                break;
            case 3:
                _shieldCount = 7;
                _weight = 2;
                break;
            default:
                _shieldCount = 0;
                break;
        }

        Player _player = FindObjectOfType<Player>();
        _player.ShieldCount += _shieldCount;
        _player.Attack(Mathf.RoundToInt(_player.ShieldCount * _weight));

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;
        
        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

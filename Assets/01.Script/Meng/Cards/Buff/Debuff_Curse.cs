using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Debuff_Curse : BattleCardBase
{
    [SerializeField] private BuffDataSO poison;
    [SerializeField] private BuffDataSO wound;
    [SerializeField] private BuffDataSO weak;
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
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void AttackEnemy()
    {
        int _poisoningCount = 0;
        int _weakCount = 0;
        int _woundCount = 0;
        
        switch (Level)
        {
            case 1:
                _poisoningCount = 4;
                _weakCount = 3;
                _woundCount = 3;
                break;
            case 2:
                _poisoningCount = 9;
                _weakCount = 7;
                _woundCount = 6;
                break;
            case 3:
                _poisoningCount = 15;
                _weakCount = 99;
                _woundCount = 99;
                break;
            default:
                _poisoningCount = 0;
                break;
        }

        Enemy _enemy = FindObjectOfType<Enemy>();
        _enemy.AddBuff(poison, _poisoningCount);
        _enemy.AddBuff(wound, _woundCount);
        _enemy.AddBuff(weak, _weakCount);

        var _effect = PoolManager.Pop(CardSO.effect);
        _effect.transform.position = CardSO.effectPosition;
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

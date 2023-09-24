using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Debuff_Poisoning : AbCard
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
        int _poisoningCount = 0;
        
        switch (Level)
        {
            case 1:
                _poisoningCount = 5;
                break;
            case 2:
                _poisoningCount = 9;
                break;
            case 3:
                _poisoningCount = 18;
                break;
            default:
                _poisoningCount = 0;
                break;
        }

        FindObjectOfType<Enemy>().AddBuff(poison, _poisoningCount);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

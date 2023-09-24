using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Debuff_Weakening : AbCard
{
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
    
    private void AttackEnemy()
    {
        int _weakCount = 0;
        
        switch (Level)
        {
            case 1:
                _weakCount = 3;
                break;
            case 2:
                _weakCount = 7;
                break;
            case 3:
                _weakCount = 15;
                break;
            default:
                _weakCount = 0;
                break;
        }

        FindObjectOfType<Enemy>().AddBuff(weak, _weakCount);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;
        
        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

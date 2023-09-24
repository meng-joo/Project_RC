using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Buff_Rest : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 0.6f));
        _seq.Insert(0f,screenImage.DOFade(1, 0.6f));

        _seq.Append(transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
        
        _seq.AppendCallback(BuffPlayer);

        return _seq.Duration() + 0.2f;
    }

    public override void Passive()
    {
        
    }

    private void BuffPlayer()
    {
        int _buffCount = 0;

        switch (Level)
        {
            case 1:
                _buffCount = 5;
                break;
            case 2:
                _buffCount = 11;
                break;
            case 3:
                _buffCount = 18;
                break;
            default:
                _buffCount = 0;
                break;
        }

        FindObjectOfType<Player>().Heal(_buffCount);

        var _effect = PoolManager.Pop(cardSO.effect);
        _effect.transform.position = cardSO.effectPosition;

        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

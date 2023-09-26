using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Buff_Defense : BattleCardBase
{
    [SerializeField] private BuffDataSO thorn;

    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 0.6f));
        _seq.Insert(0f, screenImage.DOFade(1, 0.6f));

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
        int _shieldCount = 0;

        switch (Level)
        {
            case 1:
                _buffCount = 0;
                _shieldCount = 6;
                break;
            case 2:
                _buffCount = 1;
                _shieldCount = 8;
                break;
            case 3:
                _buffCount = 5;
                _shieldCount = 12;
                break;
            default:
                _buffCount = 0;
                break;
        }

        if (Level > 1)
            FindObjectOfType<Player>().AddBuff(thorn, _buffCount);
        FindObjectOfType<Player>().ShieldCount += _shieldCount;

        var _effect = PoolManager.Pop(CardSO.effect);
        _effect.transform.position = CardSO.effectPosition;

        BattleManager.CurrentActiveSlotCount--;
        DiscardCard(transform.parent.gameObject);
    }
}

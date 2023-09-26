using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Buff_Strength : BattleCardBase
{
    [SerializeField] private BuffDataSO strength;
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();
        _seq.Insert(0f,screenImage.DOFade(1, 0.6f));

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
                _buffCount = 1;
                break;
            case 2:
                _buffCount = 3;
                break;
            case 3:
                _buffCount = 3;
                break;
            default:
                _buffCount = 0;
                break;
        }

        FindObjectOfType<Player>().AddBuff(strength, _buffCount);

        var _effect = PoolManager.Pop(CardSO.effect);
        _effect.transform.position = CardSO.effectPosition;
        
        BattleManager.CurrentActiveSlotCount--;
        if (Level <= 2)
            DiscardCard(transform.parent.gameObject);
    }
}

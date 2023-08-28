using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestCard2 : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Insert(0f, screenImage.DOFade(1, 1f));
        _seq.AppendCallback(() =>
        {
            FindObjectOfType<Player>().Attack(0);
            BattleManager _battleManager = FindObjectOfType<BattleManager>();
            _battleManager.CurrentActiveSlotCount--;
        });
        _seq.Append(screenImage.DOFade(0, 0.2f));

        return 2;
    }

    public override void Passive()
    {
        
    }

    public override void Upgrade()
    {
        
    }
}

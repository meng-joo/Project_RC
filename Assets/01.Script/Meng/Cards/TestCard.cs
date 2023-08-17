using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestCard : AbCard
{
    public override float CardSkill()
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveY(transform.position.y + 90, 1f));
        _seq.Insert(0f,screenImage.DOFade(1, 0.8f));
        _seq.AppendInterval(0.1f);
        _seq.AppendCallback(() =>
        {
            DiscardCard(gameObject);
        });

        return 2;
    }

    public override void Passive()
    {
        
    }
}

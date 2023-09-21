using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EffectManager : MonoSingleTon<EffectManager>
{
    private Tweener tweener;
    
    public void TimeSlowEffect(float _duration, float _value)
    {
        tweener?.Kill();
        
        Sequence _seq = DOTween.Sequence();
        
        _seq.Append(tweener = DOTween.To(() => 1, _scale => Time.timeScale = _scale, _value, 0.1f).SetUpdate(true));
        _seq.AppendInterval(_duration).SetUpdate(true);
        _seq.Append(tweener = DOTween.To(() => _value, _scale => Time.timeScale = _scale, 1, 0.1f).SetUpdate(true));
    }
}

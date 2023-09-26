using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EffectManager : MonoSingleTon<EffectManager>
{
    [SerializeField] private Image fadeInImage;
    public CinemachineVirtualCamera virtualCamera;
    
    private Tweener tweener;
    
    public void TimeSlowEffect(float _duration, float _value)
    {
        tweener?.Kill();
        
        Sequence _seq = DOTween.Sequence();
        
        _seq.AppendInterval(0.1f).SetUpdate(true);
        _seq.Append(tweener = DOTween.To(() => 1, _scale => Time.timeScale = _scale, _value, 0.1f).SetUpdate(true));
        _seq.AppendInterval(_duration).SetUpdate(true);
        _seq.Append(tweener = DOTween.To(() => _value, _scale => Time.timeScale = _scale, 1, 0.1f).SetUpdate(true));
    }

    public float FadeInEffect(float _duration, Action<bool> _action, bool _isOn)
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(fadeInImage.DOFade(1, _duration / 2));
        _seq.AppendCallback(() => _action.Invoke(_isOn));
        _seq.Append(fadeInImage.DOFade(0, _duration / 2));

        return _seq.Duration();
    }
    
    public void TriggerShake(float amplitude, float frequency, float duration)
    {
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // 쉐이크를 트리거합니다.
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;

        // 지정된 시간 이후 쉐이크를 해제합니다.
        StartCoroutine(StopShake(duration));
    }

    private IEnumerator StopShake(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        // 쉐이크를 해제합니다.
        CinemachineBasicMultiChannelPerlin noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}

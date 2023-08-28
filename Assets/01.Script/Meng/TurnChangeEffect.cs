using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnChangeEffect : MonoBehaviour
{
    [SerializeField] private float panelDownDeley;
    
    private Image turnChangePanel;
    private TextMeshProUGUI 전;    
    private TextMeshProUGUI 개;
    
    void Start()
    {
        turnChangePanel = transform.Find("TurnPanel_1").GetComponent<Image>();
        전 = transform.Find("Text_전").GetComponent<TextMeshProUGUI>();
        개 = transform.Find("Text_개").GetComponent<TextMeshProUGUI>();

        ResetPosition();
    }

    void ResetPosition()
    {
        turnChangePanel.transform.localPosition = new Vector3(0, 650, 0);
        전.transform.localPosition = new Vector3(-1100, 0, 0);
        개.transform.localPosition = new Vector3(1100, 0, 0);
    }

    public float ChangingEffect(string _str)
    {
        Sequence _seq = DOTween.Sequence();

        _seq.Append(turnChangePanel.transform.DOLocalMoveY(0, panelDownDeley));
        _seq.Append(전.transform.DOLocalMoveX(-85, 1f));
        _seq.Join(개.transform.DOLocalMoveX(85, 1f));

        _seq.AppendInterval(0.4f);

        _seq.Append(전.transform.DOLocalMoveX(-1100, .7f).SetEase(Ease.InQuart));
        _seq.Join(개.transform.DOLocalMoveX(1100, .7f).SetEase(Ease.InQuart));
        _seq.Append(turnChangePanel.transform.DOLocalMoveY(650, .7f).SetEase(Ease.InQuart));

        _seq.AppendCallback(ResetPosition);
        
        return _seq.Duration();
    }
}
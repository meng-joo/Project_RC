using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public abstract class AbCard : TopCard, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Transform root;
    
    private Camera mainCam;
    private BattleManager battleManager;
    private ExpUI expUI;

    private bool onDraging = false;
    
    protected BattleManager BattleManager
    {
        get
        {
            battleManager ??= FindObjectOfType<BattleManager>();
            return battleManager;
        }
    }
    
    public override void Init_Pop()
    {
        mainCam ??= Camera.main;
        expUI ??= FindObjectOfType<ExpUI>();
        base.Init_Pop();
    }

    public override void Init_Push()
    {
        
    }

    #region Card Effect
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (BattleManager.IsPlayerTurn)
        {
            transform.localScale = Vector3.one;
            OutLineEffect(false);
            expUI.FoldPopupCardInfo(CardInfo.cardPoolType);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!BattleManager.IsPlayerTurn) return;
        root ??= transform.root;
        onDraging = true;
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        root.BroadcastMessage("BeginDrag", transform.parent, SendMessageOptions.DontRequireReceiver);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (BattleManager.IsPlayerTurn || !onDraging)
            {
                OutLineEffect(true);
                expUI.PopupCardInfo(CardInfo.cardPoolType);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.IsPlayerTurn) return;
        var screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f;
        transform.parent.position = mainCam.ScreenToWorldPoint(screenPoint);
        root.BroadcastMessage("Drag", transform.parent, SendMessageOptions.DontRequireReceiver);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        root.BroadcastMessage("EndDrag", transform.parent, SendMessageOptions.DontRequireReceiver);
        onDraging = false;
    }
    public float ConsumptionEffect()
    {
        var _seq = DOTween.Sequence();

        _seq.Append(transform.DOLocalMoveX(transform.localPosition.x + 170f, 0.4f).SetEase(Ease.InBack));
        _seq.Join(transform.DOScale(0, 0.4f).SetEase(Ease.InBack));
        _seq.Join(screenImage.DOFade(1, 0.2f));

        return _seq.Duration();
    }

    #endregion
    
    #region Card Function
    public abstract float CardSkill();
    public abstract void Passive();

    public void DiscardCard(params GameObject[] _card)
    {
        foreach (var VARIABLE in _card)
        {
            PoolManager.Push(VARIABLE.GetComponentInChildren<AbCard>().CardInfo.cardPoolType, VARIABLE);
            BattleManager.arrange.Children.Remove(VARIABLE.transform);
            BattleManager.currentSlotCount--;
        }
    }

    #endregion
}
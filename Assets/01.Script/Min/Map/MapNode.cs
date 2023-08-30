using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;
using DG.Tweening;

public enum NodeStates
{
    Locked,
    Visited,
    Attainable
}

public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler //, IPointerExitHandler
{
    public Image nodeimage;
    public SpriteRenderer visitedCircle;
    public Image circleImage;
    public Image visitedCircleImage;

    public NodeStates nodeStates;

    public Node Node { get; private set; }

    private void Start()
    {
    }
    public void SetUp(Node node)
    {
        Node = node;
       // Debug.Log(node);

        if (visitedCircle != null)
        {
            visitedCircle.color = MapViewUI.Instance.visitedColor;
            visitedCircle.gameObject.SetActive(false);
        }

        if (circleImage != null)
        {
            circleImage.color = MapViewUI.Instance.visitedColor;
            circleImage.gameObject.SetActive(false);
        }

        SetState(NodeStates.Locked);
    }
    public void SetState(NodeStates nodeStates)
    {
        if (visitedCircle != null) visitedCircle.gameObject.SetActive(false);
        if (circleImage != null) circleImage.gameObject.SetActive(false);

        switch (nodeStates)
        {
            case NodeStates.Locked:
                if (nodeimage != null)
                {
                    Debug.Log("락연출");
                    nodeimage.DOKill();
                    nodeimage.color = MapViewUI.Instance.lockedColor;
                }
                break;
            case NodeStates.Visited:
                Debug.Log("아니ㅣ여기까지는 들어와야하는거아님");

                if (nodeimage != null)
                {
                    Debug.Log("방문연출");
                    nodeimage.DOKill();
                    nodeimage.color = MapViewUI.Instance.visitedColor;
                }

                if (visitedCircle != null) visitedCircle.gameObject.SetActive(true);

                if (circleImage != null) circleImage.gameObject.SetActive(true);

                break;
            case NodeStates.Attainable:
                if (nodeimage != null)
                {
                    Debug.Log("다음방문할곳연출");
                    nodeimage.color = MapViewUI.Instance.lockedColor;
                    nodeimage.DOKill();
                    nodeimage.DOColor(MapViewUI.Instance.visitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                }
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("올라감");
       // throw new System.NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("내려감");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("클릭");
        PlayerTracker.Instance.SelectNode(this);
    }

    public void ShowSwirlAnimation()
    {
        if (visitedCircleImage == null)
            return;

        const float fillDuration = 0.3f;
        visitedCircleImage.fillAmount = 0;

        DOTween.To(() => visitedCircleImage.fillAmount, x => visitedCircleImage.fillAmount = x, 1f, fillDuration);
    }
}

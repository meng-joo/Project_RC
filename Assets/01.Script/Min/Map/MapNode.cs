using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;

public enum NodeStates
{
    Locked,
    Visited,
    Attainable
}

public class MapNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler //, IPointerExitHandler
{
    public SpriteRenderer sr;
    public Image nodeimage;
    public SpriteRenderer visitedCircle;
    public Image circleImage;
    public Image visitedCircleImage;

    public NodeStates nodeStates;

    public Node Node { get; private set; }

    private void Start()
    {
        nodeimage = GetComponent<Image>();
   //     sr = GetComponent<Image>().sprite;
    }
    public void SetUp(Node node)
    {
        Node = node;
        Debug.Log(node);

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
        switch (nodeStates)
        {
            case NodeStates.Locked:
                break;
            case NodeStates.Visited:
                break;
            case NodeStates.Attainable:
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
        PlayerTracker.Instance.SelectNode(Node);
    }
    //public void ShowSwirlAnimation()
    //{
    //    if (visitedCircleImage == null)
    //        return;

    //    const float fillDuration = 0.3f;
    //    visitedCircleImage.fillAmount = 0;

    //    DOTween.To(() => visitedCircleImage.fillAmount, x => visitedCircleImage.fillAmount = x, 1f, fillDuration);
    //}
}

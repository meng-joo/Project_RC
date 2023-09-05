using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public enum NodeStates
{
    Locked,
    Visited,
    Attainable
}

public class GridNode : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public Vector2 pos;

    public bool isVisible;

    public MapSO mapSO;

    public SpriteRenderer sr;

    public List<GridNode> connectionNodeList = new List<GridNode>();

    public int connetionCount;

    public NodeStates nodeStates;

    public SpriteRenderer visitedCircle;

    public Image circleImage;
    public Image visitedCircleImage;

    public float initialScale;
    private const float HoverScaleFactor = 1.2f;
    private void OnEnable()
    {
        connetionCount = Random.Range(2, 3);
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (sr != null) initialScale = sr.transform.localScale.x;
    }

    public void Set(int x, int y, MapSO mapSO)
    {
        this.mapSO = mapSO;
        this.pos.x = x;
        this.pos.y = y;

        isVisible = false;
        SetGridActive();
    }
   
    public void SetGridActive()
    {
        if (isVisible)
        {
            Debug.Log("佃像");
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("襖像");
            gameObject.SetActive(false);
        }
    }

    public void SetGridInfo()
    {
        sr.sprite =  mapSO.mapSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GridNode"))
        {
            if (connectionNodeList.Count < connetionCount)
            {
                connectionNodeList.Add(collision.GetComponent<GridNode>());
            }
        }
    }

    public void SetState(NodeStates state)
    {
        nodeStates = state;

      //  if (visitedCircle != null) visitedCircle.gameObject.SetActive(false);
        //if (circleImage != null) circleImage.gameObject.SetActive(false);

        switch (state)
        {
            case NodeStates.Locked:
                if (sr != null)
                {
                    sr.DOKill();
                    sr.color = MapGenerator.Instance.lockedColor;
                }
                break;
            case NodeStates.Visited:
                if (sr != null)
                {
                    sr.DOKill();
                    sr.color = MapGenerator.Instance.visitedColor;
                }

                if (visitedCircle != null) visitedCircle.gameObject.SetActive(true);
                if (circleImage != null) circleImage.gameObject.SetActive(true);
                break;
            case NodeStates.Attainable:
                // start pulsating from visited to locked color:
                if (sr != null)
                {
                    sr.color = MapGenerator.Instance.lockedColor;
                    sr.DOKill();
                    sr.DOColor(MapGenerator.Instance.visitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                }
                break;
        }
    }

    public void ShowSwirlAnimation()
    {
        if (visitedCircleImage == null)
            return;

        const float fillDuration = 0.3f;
        visitedCircleImage.fillAmount = 0;

        DOTween.To(() => visitedCircleImage.fillAmount, x => visitedCircleImage.fillAmount = x, 1f, fillDuration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sr != null)
        {
            Debug.Log("けいいししいしいしいい");
            sr.transform.DOKill();
            sr.transform.DOScale(initialScale * HoverScaleFactor, 0.3f);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (sr != null)
        {
            Debug.Log("けいいし");
            sr.transform.DOKill();
            sr.transform.DOScale(initialScale, 0.3f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("適遣");
        MapPlayerTracker.Instance.SelectNode(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (sr != null)
        {
            sr.transform.DOKill();
            sr.transform.DOScale(initialScale, 0.3f);
        }
    }
}

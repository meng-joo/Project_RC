using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoSingleTon<PlayerTracker>
{
    public int currentNodeIndex = 0;
    public float enterNodeDelay = 1f;

    public bool Locked = false;   
    private void Start()
    {
    }

    public void SelectNode(MapNode node)
    {
        if (Locked)
        {
            return;
        }

        if (node.nodeStates == NodeStates.Locked || node.nodeStates == NodeStates.Visited)
        {
            Debug.Log("Selected node cannot be accessed");
            return;
        }

        Locked = true;
        SendPlayerToNode(node);

        //     EnterNode();
    }
    public void SendPlayerToNode(MapNode node)
    {
        Debug.Log("일단OK");
        currentNodeIndex++;

        node.nodeStates = NodeStates.Visited;
        Debug.Log(node.nodeStates);
        node.SetState(node.nodeStates);

        MapNode nextNode = MapViewUI.Instance.mapNodeList[currentNodeIndex];
        nextNode.nodeStates = NodeStates.Attainable;
        nextNode.SetState(nextNode.nodeStates);


        node.ShowSwirlAnimation();
        DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(node)) ;
        DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => Locked = false);
    
    }
    public void EnterNode(MapNode node)
    {
        switch (node.Node.mapSO.mapType)
        {
            case MapType.Battle:
                Debug.Log("맞짱");
                break;
            case MapType.Event:
                Debug.Log("이벤트");
                break;
            case MapType.Shop:
                Debug.Log("상상점");
                break;
            case MapType.Boss:
                break;
            default:
                break;
        }
    }

}

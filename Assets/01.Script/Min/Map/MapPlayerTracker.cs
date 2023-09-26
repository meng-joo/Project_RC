using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class MapPlayerTracker : MonoSingleTon<MapPlayerTracker>
{
    public bool lockAfterSelecting = false;

    public float enterNodeDelay = 1f;
    public bool Locked { get; set; }
    public void SelectNode(GridNode node)
    {
        if (Locked) return;

        if (node.nodeStates == NodeStates.Attainable)
        {
            SendPlayerToNode(node);
        }
        else
        {
            PlayWarningThatNodeCannotBeAccessed();
        }
    }

    private void SendPlayerToNode(GridNode node)
    {
        Locked = lockAfterSelecting;
        //mapManager.CurrentMap.path.Add(mapNode.Node.point);
        //view.SetAttainableNodes();
        //view.SetLineColors();
        node.ShowSwirlAnimation();
        node.SetState(NodeStates.Visited);
        NextNodeSet(node);
        DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(node));
      //  DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => NextNodeSet(node));
    }

    private void NextNodeSet(GridNode node)
    {
        for (int i = 0; i < node.connectionNodeList.Count; i++)
        {
            node.connectionNodeList[i].SetState(NodeStates.Attainable);
        }
    }
    private static void EnterNode(GridNode node)
    {
        Debug.Log(node.mapSO.mapType);
        switch (node.mapSO.mapType)
        {
            case MapType.Battle:
                Debug.Log("배틀맵");

                //FindObjectOfType<BattleManager>().StartBattle();
                //MapUIManager.Instance.OnBattle();
                break;
            case MapType.Event:
                Debug.Log("이벤트맵");
                break;
            case MapType.Shop:
                break;
            case MapType.Boss:
                break;
        }
    }

    private void PlayWarningThatNodeCannotBeAccessed()
    {
        Debug.Log("Selected node cannot be accessed");
    }
}

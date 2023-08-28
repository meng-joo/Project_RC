using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTracker : MonoSingleTon<PlayerTracker>
{
    public int currentNodeIndex = 0;

    public bool Locked { get; set; }

    public void SelectNode(Node node)
    {
        if (Locked)
        {
            PlayWarningThatNodeCannotBeAccessed();
            return;
        }

        SendPlayerToNode(node);

        //     EnterNode();
    }
    public void SendPlayerToNode(Node node)
    {
        currentNodeIndex++;
    }
    public void EnterNode(Node node)
    {
        switch (node.mapSO.mapType)
        {
            case MapType.Battle:
                Debug.Log("��¯");
                break;
            case MapType.Event:
                Debug.Log("�̺�Ʈ");
                break;
            case MapType.Shop:
                Debug.Log("�����");
                break;
            case MapType.Boss:
                break;
            default:
                break;
        }
    }
    private void PlayWarningThatNodeCannotBeAccessed()
    {
        Debug.Log("Selected node cannot be accessed");
    }
}

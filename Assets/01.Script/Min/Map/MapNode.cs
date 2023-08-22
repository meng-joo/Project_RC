using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TreeEditor.TreeEditorHelper;

public class MapNode : MonoBehaviour
{
    public SpriteRenderer sr;
    public Image image;
    public SpriteRenderer visitedCircle;
    public Image circleImage;
    public Image visitedCircleImage;

    public Node Node { get; private set; }

    public void SetUp(Node node)
    {
        Node = node;
   

     //   SetState(NodeStates.Locked);
    }

}

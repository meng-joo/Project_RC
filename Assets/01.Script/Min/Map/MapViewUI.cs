using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UI.Extensions;
using Unity.VisualScripting;
using UnityEngine.Profiling.Memory.Experimental;

public class MapViewUI : MonoSingleTon<MapViewUI>
{
    public GameObject uiNode;

    public List<MapNode> mapNodeList = new List<MapNode>();
    public List<Vector2> nodePosList = new List<Vector2>();


    public Transform parentTrm;

    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;


    [SerializeField] private float spacing;

    public UILineRenderer lineRenderer;

    [Tooltip("Node Visited or Attainable color")]
    public Color32 visitedColor = Color.white;
    [Tooltip("Locked node color")]
    public Color32 lockedColor = Color.gray;

    void Start()
    {
        CreateNodes(MapGenerator.Instance.mapKinList);
        SetData();
    }
    private void Update()
    {
    }

    public void CreateNodes(IEnumerable<Node> nodes)
    {
        foreach (var node in nodes.Select((value, index) => (value, index)))
        {
            float randomY = Random.Range(minHeight, maxHeight);

            var mapNode = CreateMapNode(node.value);

            mapNode.GetComponent<RectTransform>().localPosition = new Vector2(200 + spacing * node.index , randomY);
            Debug.Log(mapNode.GetComponent<RectTransform>().anchoredPosition);

            nodePosList.Add(mapNode.GetComponent<RectTransform>().localPosition);
            lineRenderer.Points = nodePosList.ToArray();
            mapNodeList.Add(mapNode);
        }
    }

    public MapNode CreateMapNode(Node node)
    {
        var mapNodeObject = Instantiate(uiNode, transform);
        mapNodeObject.transform.SetParent(parentTrm);
        mapNodeObject.transform.localScale = Vector3.one;
        var mapNode = mapNodeObject.GetComponent<MapNode>();
        mapNode.SetUp(node);
        return mapNode;
    }

    public void SetData()
    {
        List<Image> imageList = new();

        foreach (var item in mapNodeList)
        {
            imageList.Add(item.transform.GetChild(0).GetComponent<Image>());
        }

        foreach (var item in mapNodeList.Select((value, index) => (value, index)))
        {
            imageList[item.index].sprite =  item.value.Node.mapSO.mapSprite;
            imageList.Add(item.value.transform.GetChild(0).GetComponent<Image>()); //GeC GetComponent<Image>());
        }
    }
}

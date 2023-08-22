using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.UI.Extensions;
using Unity.VisualScripting;

public class MapViewUI : MonoBehaviour
{
    public GameObject uiNode;

    public List<MapNode> MapNodes = new List<MapNode>();

    public MapGenerator mapGenerator;

    public Transform parentTrm;
    public Transform startTrm;


    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;


    [SerializeField] private float spacing;

    public UILineRenderer xss;

    void Start()
    {
        CreateNodes(mapGenerator.mapKinList);
    }
    private void Update()
    {
    }

    public List<Vector2> s = new List<Vector2>();
    public void CreateNodes(IEnumerable<Node> nodes)
    {
        foreach (var node in nodes.Select((value, index) => (value, index)))
        {
            float randomY = Random.Range(minHeight, maxHeight);

            var mapNode = CreateMapNode(node.value);

            mapNode.GetComponent<RectTransform>().localPosition = new Vector2(200 + spacing * node.index , randomY);
            Debug.Log(mapNode.GetComponent<RectTransform>().anchoredPosition);

            s.Add(mapNode.GetComponent<RectTransform>().localPosition);

            Debug.Log(xss);
            xss.Points = s.ToArray();

         //   uILineRenderer.Points.AddRange(v);
            MapNodes.Add(mapNode);


            //  mapNode.GetComponent<RectTransform>().anchoredPosition.Set() = new Vector2(3, 0);

            //mapNode.GetComponent<RectTransform>().anchoredPosition =new Vector2()

            //float mapNodePosX = mapNode.GetComponent<RectTransform>().localPosition.x;
            //float mapNodePosY = mapNode.GetComponent<RectTransform>().localPosition.y;

            //mapNodePosX = mapNodePosX + randomX;
            //mapNodePosY = mapNodePosY + randomY;

            //mapNode.GetComponent<RectTransform>().localPosition = new Vector3(mapNodePosX, mapNodePosY);

            //위치 + 패딩
            //mapNode.GetComponent<RectTransform>().localPosition.y + randomY;
            //mapNode.GetComponent<RectTransform>().localPosition.x + randomX;
        }
    }

    public MapNode CreateMapNode(Node node)
    {
        var mapNodeObject = Instantiate(uiNode, startTrm);
        mapNodeObject.transform.SetParent(parentTrm);
        mapNodeObject.transform.localScale = Vector3.one;
        var mapNode = mapNodeObject.GetComponent<MapNode>();
        //var blueprint = GetBlueprint(node.blueprintName);
      //  mapNode.SetUp(node, blueprint);
        return mapNode;
    }
}

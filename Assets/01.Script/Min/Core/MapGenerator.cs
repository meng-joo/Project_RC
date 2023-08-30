using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MapGenerator : MonoSingleTon<MapGenerator>
{
    public List<Node> mapKinList = new List<Node>();

    [SerializeField] private ChaterSO currentChapter;

   // public MapManager mapManager;

    [SerializeField] private Node battleNode;
    [SerializeField] private Node eventNode;
    [SerializeField] private Node shopNode;
    [SerializeField] private Node bossNode;

    
    void Awake()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        CreateNode(battleNode, eventNode, shopNode);
        ShuffleMapNodeList();
    }

    public void CreateNode(Node battleNode, Node shopNode, Node eventNode)
    {
        for (int i = 0; i < currentChapter.battleMapCnt; i++)
        {
            mapKinList.Add(battleNode);
        }
        for (int i = 0; i < currentChapter.shopMapCnt; i++)
        {
            mapKinList.Add(shopNode);

        }
        for (int i = 0; i < currentChapter.eventMapCnt; i++)
        {
            mapKinList.Add(eventNode);

        }
    }
    public void ShuffleMapNodeList() 
    {
        var shuffle = mapKinList.OrderBy(a => Guid.NewGuid()).ToList();
        mapKinList = shuffle;
        AddBossNode();
        Debug.Log("suffle");
    }
    public void AddBossNode()
    {
        mapKinList.Add(bossNode);
    }


}

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoSingleTon<MapGenerator>
{
    [Header("Colors")]
    [Tooltip("Node Visited or Attainable color")]
    public Color32 visitedColor = Color.white;
    [Tooltip("Locked node color")]
    public Color32 lockedColor = Color.gray;

    public AllMapTypeSO allmapTypeSO;
    public MapStageSettingSO mapSettingSO;
    public GridNode[,] customArray;
    public GameObject nodePrefab;
    public Transform parentTrm;

    public List<GridNode> mapGridNodeList = new List<GridNode>();
    public List<MapSO> mapSetList = new List<MapSO>();

    public MapSO shopMap;
    public MapSO eventMap;
    public MapSO battleMap;
    public MapSO bossMap;

    int endCnt = 0;

    public int padding;

    private int centerIndex = 0;
    void Start()
    {
        endCnt = mapSettingSO.mapCnt;
        centerIndex = mapSettingSO.garoCnt * mapSettingSO.seroCnt / 2;
        customArray = new GridNode[mapSettingSO.seroCnt, mapSettingSO.garoCnt];
        SetMapListSO();
        CreateNode();

        IndexSetting();

        SetCenterNode();

        SetBossNode();
    }

    public void CreateNode() ///생성
    {
        for (int i = 0; i < mapSettingSO.garoCnt; i++)
        {
            for (int j = 0; j < mapSettingSO.seroCnt; j++)
            {
                GameObject obj = Instantiate(nodePrefab, parentTrm);
                obj.transform.localPosition = new Vector2(i * padding, j * padding);
                obj.GetComponent<GridNode>().Set(i, j, GridNodeMapSO());
                obj.GetComponent<GridNode>().SetGridInfo();
                obj.GetComponent<GridNode>().SetState(NodeStates.Locked);
                customArray[i, j] = obj.GetComponent<GridNode>();
                mapGridNodeList.Add(customArray[i, j].GetComponent<GridNode>());
            }
        }
    }

    public void IndexSetting() ///가로 수만큼 리스트 인덱스 만듬
    {
        for (int i = 0; i < mapSettingSO.garoCnt; i++)
        {
            RandomActiveNode(i);
        }
    }

    public void RandomActiveNode(int index)
    {
        int rowRandomActiveCnt = Random.Range(mapSettingSO.nodeActiveMinCnt, mapSettingSO.nodeActiveMaxCnt);

        HashSet<int> randomIndexList = new HashSet<int>();

        for (int i = 0; i < rowRandomActiveCnt;)
        {
            int randomNumber = Random.Range(0, mapSettingSO.garoCnt);

            if (randomIndexList.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, mapSettingSO.garoCnt);
            }
            else
            {
               // Debug.Log("리스트에 추가되는 랜덤넘버 +  " + randomNumber);
                randomIndexList.Add(randomNumber);
                i++;
            }
        }


        foreach (var item in randomIndexList)
        {
            // Debug.Log("item" + item);
            //Debug.Log("postitonf" + customArray[item, index].pos);
            customArray[item, index].isVisible = true;
            customArray[item, index].SetGridActive();
        }
    }

    public void SetCenterNode()
    {
        mapGridNodeList[centerIndex].isVisible = true;
        mapGridNodeList[centerIndex].SetGridActive();

        mapGridNodeList[centerIndex].mapSO = battleMap;

        mapGridNodeList[centerIndex].SetState(NodeStates.Attainable);
    }
    
    public List<int> bossIndexList = new List<int>();
    public void SetBossNode()
    {
        bossIndexList.Add(0);
        bossIndexList.Add(0 + mapSettingSO.seroCnt);
        bossIndexList.Add(mapSettingSO.mapCnt - mapSettingSO.seroCnt);
        bossIndexList.Add(mapSettingSO.mapCnt - 1);

        int randomIdx = Random.Range(0, bossIndexList.Count);

        Debug.Log("보스랜덤위치" + randomIdx);

        //mapGridNodeList[bossIndexList[randomIdx]].gameObject.SetActive(true);
        mapGridNodeList[bossIndexList[randomIdx]].isVisible = true;
        mapGridNodeList[bossIndexList[randomIdx]].SetGridActive();


        mapGridNodeList[bossIndexList[randomIdx]].mapSO = bossMap;
        mapGridNodeList[bossIndexList[randomIdx]].SetGridInfo();
    }
    public void SetMapListSO()
    {
        for (int i = 0; i < mapSettingSO.eventMapCnt; i++)
        {
            mapSetList.Add(eventMap);
        }
        for (int i = 0; i < mapSettingSO.shopMapCnt; i++)
        {
            mapSetList.Add(shopMap);
        }
        for (int i = 0; i < mapSettingSO.battleMapCnt; i++)
        {
            mapSetList.Add(battleMap);
        }
    }

    public MapSO GridNodeMapSO()
    {
        int randomIndex = Random.Range(0, endCnt);
        MapSO mapSO = mapSetList[randomIndex];
        mapSetList.RemoveAt(randomIndex);
        endCnt--;
        return mapSO;   
    }

    public void SetConnetionNode()
    {

    }
}

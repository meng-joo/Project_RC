using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Resources;

public static class PoolManager
{
    private static PoolDataSO poolData = null;//Ǯ�� ������
    private static Dictionary<PoolType, LocalPoolManager> localPoolDic = new();//PoolType���� �˻��ϱ� ���� ��ųʸ�
    private static GameObject root = null;

    private static void Init()
    {
        foreach (LocalPoolManager manager in localPoolDic.Values)
        {
            manager.Destroy();
        }
        localPoolDic.Clear();

        poolData = ResourceManager.Load<PoolDataSO>("Pool/PoolDataSO");

        GameObject newRoot = new();
        newRoot.name = "@POOL_ROOT";
        root = newRoot;

        for (int i = 0; i < poolData.poolDatas.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(root.transform);
            LocalPoolManager localPool = obj.AddComponent<LocalPoolManager>();
            PoolDataSO.PoolData data = poolData.poolDatas[i];
            localPool.Init(data.InitCount, data.PoolAbleObject, data.PoolType);
            localPoolDic.Add(data.PoolType, localPool);
            localPool.name = $"POOL_LOCAL[{data.PoolType}]";
        }
    }

    /// <summary>
    /// Type�� �´� ������Ʈ ��������
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject Pop(PoolType type)
    {
        if (root == null) Init();
        return localPoolDic[type].Pop().gameObject;
    }
    /// <summary>
    /// Type�� �°� ������Ʈ �ֱ�
    /// </summary>
    /// <param name="type"></param>
    /// <param name="obj"></param>
    public static void Push(PoolType type, GameObject obj)
    {
        if (root == null) Init();
        localPoolDic[type].Push(obj.GetComponent<PoolAbleObject>());
    }

}
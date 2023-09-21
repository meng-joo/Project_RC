using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPoolManager : MonoBehaviour
{
    PoolAbleObject poolTarget;
    public PoolAbleObject PoolTarget { get { return poolTarget; } set { poolTarget = value; } }
    Queue<PoolAbleObject> poolQueue = new Queue<PoolAbleObject>();
    PoolType type;
    /// <summary>
    /// 초기 지정 갯수만큼 오브젝트 인스턴스화
    /// </summary>
    /// <param name="갯수"></param>
    /// <param name="오브젝트"></param>
    /// <param name="풀 타입"></param>
    public void Init(int count, PoolAbleObject targetObj, PoolType poolType)
    {
        type = poolType;
        poolTarget = targetObj;
        for (int i = 0; i < count; i++)
        {
            //Debug.Log(poolType);
            PoolAbleObject obj = Instantiate(PoolTarget, transform);
            obj.name = obj.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            obj.PoolType = type;
            poolQueue.Enqueue(obj);
        }
    }
    /// <summary>
    /// 꺼내오기
    /// </summary>
    /// <returns></returns>
    public PoolAbleObject Pop()
    {
        PoolAbleObject obj;
        if (poolQueue.Count > 0)
        {
            obj = poolQueue.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate(poolTarget, transform);
            obj.name = obj.name.Replace("(Clone)", "");
            obj.PoolType = type;
        }
        obj.Init_Pop();
        return obj;
    }
    /// <summary>
    /// 넣기
    /// </summary>
    /// <param name="obj"></param>
    public void Push(PoolAbleObject obj)
    {
        obj.Init_Push();
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    public void Destroy()
    {
        poolQueue.Clear();
        Destroy(this);
    }
}
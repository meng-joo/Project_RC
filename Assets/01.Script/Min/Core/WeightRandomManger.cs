using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class WeightRandomManger : MonoSingleTon<WeightRandomManger>
{
    public CardTierListSO testList;// = new List<CardSO>();
    //public List<CardSO> testList = new List<CardSO>();
    //public List<double> WeightList = new List<double>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            WeightRandom(testList);
        }
    }

    public CardSO WeightRandom(CardTierListSO target)//List<CardSO> target)
    {
        if (target == null)
        {
            Debug.Log("The tier list is empty");
            return null;
        }


        double totalWeight = 0;

        foreach (var item in target.tierCardList)
        {
            totalWeight += item.randomWeight;
        }

        Debug.Log("총 가중치의 합" + totalWeight);

        double randomValue = Random.Range(0f, 1f);

        Debug.Log("랜덤밸류" + randomValue);

        randomValue *= totalWeight;


        foreach (var item in target.tierCardList)
        {
            randomValue -= item.randomWeight;

            if (randomValue <= 0f)
            {
                Debug.Log("반환된 카드값" + item);
                return item;
            }
        }
        return null;

        ////2. 주어진 가중치를 백분율로 치환 (가중치 / 총 가중치)

        //List<double> l = new List<double>();
        //foreach (var item in target)
        //{
        //    l.Add(item.randomWeight / totalWeight);
        //}

        //// 3. 가중치의 오름차순으로 정렬

        //l.Sort();
        ////WeightList = l;

    }
}

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

    public CardSO WeightRandom(CardTierListSO target)
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

        randomValue *= totalWeight;

        foreach (var item in target.tierCardList)
        {
            randomValue -= item.randomWeight;

            if (randomValue <= 0f)
            {
                return item;
            }
        }
        return null;
    }
}

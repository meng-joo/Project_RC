using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class WeightRandomManger : MonoBehaviour
{
    public List<CardSO> testList = new List<CardSO>();
    //public List<double> WeightList = new List<double>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            WeightRandom(testList);
        }
    }

    public CardSO WeightRandom(List<CardSO> target)
    {
        double totalWeight = 0;

        foreach (var item in target)
        {
            totalWeight += item.randomWeight;
        }

        Debug.Log("�� ����ġ�� ��" + totalWeight);

        double randomValue = Random.Range(0f, 1f);

        Debug.Log("�������" + randomValue);

        randomValue *= totalWeight;


        foreach (var item in target)
        {
            randomValue -= item.randomWeight;

            if (randomValue <= 0f)
            {
                Debug.Log("��ȯ�� ī�尪" + item);
                return item;
            }
        }
        return null;

        ////2. �־��� ����ġ�� ������� ġȯ (����ġ / �� ����ġ)

        //List<double> l = new List<double>();
        //foreach (var item in target)
        //{
        //    l.Add(item.randomWeight / totalWeight);
        //}

        //// 3. ����ġ�� ������������ ����

        //l.Sort();
        ////WeightList = l;

    }
}

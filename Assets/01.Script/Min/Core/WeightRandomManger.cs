using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeightRandomManger : MonoBehaviour
{
    CardSO so;
    public  void WeightedRandom(Dictionary<CardSO, int> target)//List<Dictionary<CardSO, int>> target)
    {

        //// 1. 총 가중치 합 계산
        //double totalWeight = 0;
        //for (Pair<String, Integer> pair : target)
        //{
        //    totalWeight += pair.weight;
        //}

        //1. 총 가중치 합 계산
        float totalWeight = 0;

        foreach (var item in target)
        {
            totalWeight += item.Key.randomWeight;
        }
        //// 2. 주어진 가중치를 백분율로 치환 (가중치 / 총 가중치)
        Dictionary<CardSO, float> candidates = new();
        foreach (var item in target)
        {
            candidates.Add(so, item.Key.randomWeight / totalWeight);
        }


        //// 2. 주어진 가중치를 백분율로 치환 (가중치 / 총 가중치)
        //List<Pair<String, Double>> candidates = new ArrayList<>();
        //for (Pair<String, Integer> pair : target)
        //{
        //    candidates.add(new Pair<>(pair.word, pair.weight / totalWeight));
        //}

        //// 3. 가중치의 오름차순으로 정렬
        //candidates.sort(Comparator.comparingDouble(p->p.weight));
        //this.candidates = candidates;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

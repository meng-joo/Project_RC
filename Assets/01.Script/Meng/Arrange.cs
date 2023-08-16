using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrange : MonoBehaviour
{
    [SerializeField] List<Transform> children;
    void Start()
    {
        children = new List<Transform>();
        
        UpdateChildren();
    }

    public void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == children.Count)
            {
                children.Add(null);
            }

            var child = transform.GetChild(i);

            if (child != children[i])
            {
                children[i] = child;
            }
        }
        
        children.RemoveRange(transform.childCount, children.Count - transform.childCount);
    }

    public int GetIndexByPosition(Transform _card, int _skipIndex = -1)
    {
        int result = 0;

        for (int i = 0; i < children.Count; i++)
        {
            if (_card.position.x < children[i].position.x)
            {
                break;
            }
            else if (_skipIndex != i)
            {
                result++;
            }
        }

        return result;
    }

    public void SwapCard(int _index1, int _index2)
    {
        CanvasCentral.SwapCards(children[_index1], children[_index2]);
        UpdateChildren();
    }

    void Update()
    {
        
    }
}

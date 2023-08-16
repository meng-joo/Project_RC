using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCentral : MonoBehaviour
{
    [SerializeField] private Transform invisibleCard;

    private List<Arrange> arranges;
    void Start()
    {
        arranges = new List<Arrange>();

        var arrs = transform.GetComponentsInChildren<Arrange>();

        for (int i = 0; i < arrs.Length; i++)
        {
            arranges.Add(arrs[i]);
        }
    }

    void Update()
    {
        
    }

    public void AddCardToArrange(Arrange _arrange)
    {
        arranges.Add(_arrange);
    }
    
    public static void SwapCards(Transform _sour, Transform _dest)
    {
        Transform sourParent = _sour.parent;
        Transform destParent = _dest.parent;

        int sourIndex = _sour.GetSiblingIndex();
        int destIndex = _dest.GetSiblingIndex();
        
        _sour.SetParent(destParent);
        _sour.SetSiblingIndex(destIndex);
        
        _dest.SetParent(sourParent);
        _dest.SetSiblingIndex(sourIndex);
    }

    void SwapCardsInHierarchy(Transform _sour, Transform _dest)
    {
        SwapCards(_sour, _dest);

        arranges.ForEach(t => t.UpdateChildren());
    }

    bool ContainPos(RectTransform _rt, Vector2 _pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(_rt, _pos);
    }

    void BeginDrag(Transform _card)
    {
        SwapCardsInHierarchy(invisibleCard, _card);
    }
    void Drag(Transform _card)
    {
        var whichArrangerCard = arranges.Find(t => ContainPos(t.transform as RectTransform, _card.position));

        if (whichArrangerCard == null)
        {
              //Debug.LogError("asjfnj");
        }
        else
        {
            int invisibleCardIndex = invisibleCard.GetSiblingIndex();
            int targetIndex = whichArrangerCard.GetIndexByPosition(_card, invisibleCardIndex);

            if (invisibleCardIndex != targetIndex)
            {
                whichArrangerCard.SwapCard(invisibleCardIndex, targetIndex);
            }
        }
    }
    void EndDrag(Transform _card)
    {
        SwapCardsInHierarchy(invisibleCard, _card);
    }
}

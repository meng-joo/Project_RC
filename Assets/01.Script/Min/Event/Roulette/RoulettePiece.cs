using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RoulettePiece : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI descriptionTxt;
    public void SetUp(RoulettePieceData data)
    {
        descriptionTxt.text = data.description;
        icon.sprite = data.icon;
    }
}

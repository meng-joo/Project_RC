using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RoulettePieceData  
{
    public Sprite icon;
    public string description;
    [HideInInspector]
    public int index;           // ������ ����
    public  UnityEvent unityAction;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BuffSO")]
public class BuffDataSO : ScriptableObject
{
    public BufType bufType;

    public Sprite icon;

    [TextArea] 
    public string buffExp;
}

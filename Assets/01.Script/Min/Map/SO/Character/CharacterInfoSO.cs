using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Character")]
public class CharacterInfoSO : ScriptableObject
{
    public Sprite sprite;
    public int hp;
    public int attackPower;
}

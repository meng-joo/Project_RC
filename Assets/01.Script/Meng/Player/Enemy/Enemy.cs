using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Unit
{
    public void Start()
    {
        SetInfo();
        enemy = FindObjectOfType<Player>();
    }
    public abstract float Skill();
}

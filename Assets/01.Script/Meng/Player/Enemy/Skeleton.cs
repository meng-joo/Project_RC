using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public override float Skill()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                Attack(10);
                break;
            case 1:
                SpecialAbility();
                break;
        }
        
        return 2;
    }
}

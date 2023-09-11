using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringer : Enemy
{
    public override float Skill()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                Attack(10);
                DamageEnemy(20);
                break;
            case 1:
                SpecialAbility();
                DamageEnemy(10);
                break;
        }
        
        return 2;
    }

    private void DamageEnemy(int _damage)
    {
        enemy.Hit(10);

        DamageTextManager.CreateDamageText(10, Color.red);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeathBringer : Enemy
{
    [SerializeField] private SerializableDictionary<BufType, BuffDataSO> bufOrDebuf = new SerializableDictionary<BufType, BuffDataSO>();

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

    public override float SpecialAbility()
    {
        enemy.AddBuff(bufOrDebuf[BufType.POISON], 10);
        
        return base.SpecialAbility();
    }
    
    private void DamageEnemy(int _damage)
    {
        enemy.Hit(10);

        DamageTextManager.CreateDamageText(enemy.transform.position, 10, Color.red);
    }
}

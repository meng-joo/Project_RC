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
        enemy.AddBuff(bufOrDebuf[BufType.POISON], 5);
        
        return base.SpecialAbility();
    }
    
    private void DamageEnemy(int _damage)
    {
        Attack(_damage);
        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
    }
}

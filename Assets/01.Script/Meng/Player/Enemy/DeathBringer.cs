using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DeathBringer : Enemy
{
    [SerializeField] private SerializableDictionary<BufType, BuffDataSO> bufOrDebuf = new SerializableDictionary<BufType, BuffDataSO>();

    public void Start()
    {
        base.Start();
    }

    public override float Skill()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                DamageEnemy(12);
                break;
            case 1:
                SpecialAbility();
                break;
        }
        
        return 2;
    }

    public override float SpecialAbility()
    {
        int rand = Random.Range(1, 5);
        switch (rand)
        {
            case 1:
                enemy.AddBuff(bufOrDebuf[BufType.POISON], 4);
                break;
            case 2:
                enemy.AddBuff(bufOrDebuf[BufType.WEAK], 2);
                break;
            case 3:
                AddBuff(bufOrDebuf[BufType.STRONG], 1);
                break;
            case 4:
                AddBuff(bufOrDebuf[BufType.IRONARMOR], 2);
                break;
        }

        DamageEnemy(3);
        
        return base.SpecialAbility();
    }
    
    private void DamageEnemy(int _damage)
    {
        Attack(_damage);
        EffectManager.Instance.TimeSlowEffect(0.4f, 0.1f);
    }
}

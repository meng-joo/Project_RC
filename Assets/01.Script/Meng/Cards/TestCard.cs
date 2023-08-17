using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCard : AbCard
{
    public override float CardSkill()
    {
        DiscardCard(gameObject);
        return 0;
    }

    public override void Passive()
    {
        
    }
}

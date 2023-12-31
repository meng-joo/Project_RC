using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoSingleTon<BuffManager>
{
    public ABBuff BuffType(Unit _unit, BuffDataSO _buffDataSO, int _count)
    {
        switch (_buffDataSO.bufType)
        {
            case BufType.POISON:
                return new Debuff_Poison(_unit, _buffDataSO, _count);
            case BufType.STRONG:
                return new Buff_Strong(_unit, _buffDataSO, _count);
            case BufType.WOUND:
                return new Debuff_Wound(_unit, _buffDataSO, _count);
            case BufType.WEAK:
                return new Debuff_Weak(_unit, _buffDataSO, _count);
            case BufType.THORN:
                return new Buff_Thorn(_unit, _buffDataSO, _count);
            case BufType.IRONARMOR:
                return new Buff_IronArmor(_unit, _buffDataSO, _count);
            default:
                return null;
        }

        return null;
    }
}

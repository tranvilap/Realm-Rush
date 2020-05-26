using UnityEngine;

namespace Buffs
{
    public abstract class TowerBuffEffect : ScriptableObject
    {
        public abstract void ApplyEffect(BaseTowerBuff parent,Tower tower);
        public abstract bool RemoveEffect(BaseTowerBuff parent, Tower tower);
    } 
}

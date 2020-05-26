using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Buffs
{
    public abstract class EnemyBuffEffect : ScriptableObject
    {
        public abstract void ApplyEffect(object source,Enemy enemy);
        public abstract bool RemoveEffect(object source, Enemy enemy);
    }
}
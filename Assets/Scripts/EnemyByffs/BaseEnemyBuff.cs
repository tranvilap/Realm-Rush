using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    public enum EnemyBuffType { Default,Crystal_Debuff}
    public abstract class BaseEnemyBuff : MonoBehaviour
    {
        [SerializeField] protected EnemyBuffEffect[] buffEffects = null;
        public EnemyBuffType buffType = EnemyBuffType.Default;
        

        private int buffLevel = 0;
        private Enemy target;
        private object source;
        private bool isActivate = false;

        public int BuffLevel { get => buffLevel; protected set => buffLevel = value; }
        public Enemy Target { get => target; protected set => target = value; }
        public object Source { get => source; protected set => source = value; }
        public bool IsActivate { get => isActivate; protected set => isActivate = value; }

        public void Init(Enemy enemy, int buffLevel, object source)
        {
            Target = enemy;
            BuffLevel = buffLevel;
            Source = source;
        }

        public virtual void ApplyBuffsToTarget()
        {
            IsActivate = true;
            foreach (var buff in buffEffects)
            {
                buff.ApplyEffect(this, Target);
            }
            Target.Buffs.Add(this);
        }
        public virtual void RemoveTargetBuffs()
        {
            if (!isActivate) { return; }
            isActivate = false;
            //Be careful, this method doesn't remove buff out of enemy buff list
            foreach (var effect in buffEffects)
            {
                effect.RemoveEffect(this, Target);
            }
        }
    }
}

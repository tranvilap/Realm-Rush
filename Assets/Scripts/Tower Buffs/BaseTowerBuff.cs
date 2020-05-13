using UnityEngine;

namespace TowerBuffs
{
    public enum TowerBuffType { Default, WatchTower }
    public class BaseTowerBuff : MonoBehaviour
    {
        public TowerBuffEffect[] buffEffects;
        public TowerBuffType type = TowerBuffType.Default;
        [SerializeField] private int buffLevel = 0;

        protected Tower targetTower;


        public int BuffLevel { get => buffLevel; }
        public Tower TargetTower { get => targetTower; }

        public virtual void SetTargetTower(Tower tower, int buffLevel)
        {
            targetTower = tower;
            this.buffLevel = buffLevel;
        }

        public virtual void ApplyBuffsToTarget()
        {
            foreach (var buff in buffEffects)
            {
                buff.ApplyEffect(this, TargetTower);
            }
        }
        public virtual bool RemoveTargetBuffs()
        {
            bool didRemove = false;
            foreach (var effect in buffEffects)
            {
                if (!effect.RemoveEffect(this, TargetTower)) { didRemove = false; }
            }
            return didRemove;
        }

        protected virtual void OnDisable()
        {
            if (TargetTower != null)
            {
                RemoveTargetBuffs();
            }
        }
    }

}
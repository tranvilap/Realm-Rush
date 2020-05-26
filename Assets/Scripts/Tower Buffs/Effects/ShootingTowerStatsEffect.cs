using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
namespace Buffs
{
    [CreateAssetMenu(menuName = "Tower Buff Effect/Shooting Tower")]
    public class ShootingTowerStatsEffect : TowerBuffEffect
    {
        [SerializeField] protected StatModifier[] effectRangeMods = null;
        [SerializeField] protected StatModifier[] firingRateMods = null;
        [SerializeField] protected StatModifier[] bulletSpeedMods = null;
        [SerializeField] protected StatModifier[] powerMods = null;

        public override void ApplyEffect(BaseTowerBuff parent, Tower tower)
        {
            foreach (var mod in effectRangeMods)
            {
                var newMod = new StatModifier(mod, parent);
                tower.EffectRangeRadius.AddModifier(newMod);
            }

            if (tower is ShootingTower)
            {
                foreach (var mod in firingRateMods)
                {
                    var newMod = new StatModifier(mod, parent);
                    ((ShootingTower)tower).FiringRate.AddModifier(newMod);
                }
                foreach (var mod in bulletSpeedMods)
                {
                    var newMod = new StatModifier(mod, parent);
                    ((ShootingTower)tower).BulletSpeed.AddModifier(newMod);
                }
                foreach (var mod in powerMods)
                {
                    var newMod = new StatModifier(mod, parent);
                    ((ShootingTower)tower).Power.AddModifier(newMod);
                }
            }
            else if(tower is LaserTower)
            {
                foreach (var mod in powerMods)
                {
                    var newMod = new StatModifier(mod, parent);
                    ((LaserTower)tower).DPS.AddModifier(newMod);
                }
            }
        }

        public override bool RemoveEffect(BaseTowerBuff parent, Tower tower)
        {
            bool didRemove = false;
            didRemove = tower.EffectRangeRadius.RemoveModifiersFromSource(parent);

            if (tower is ShootingTower)
            {
                if (!didRemove)
                {
                    didRemove = ((ShootingTower)tower).FiringRate.RemoveModifiersFromSource(parent)
                                && ((ShootingTower)tower).BulletSpeed.RemoveModifiersFromSource(parent)
                                && ((ShootingTower)tower).Power.RemoveModifiersFromSource(parent);
                }
                else
                {
                    ((ShootingTower)tower).FiringRate.RemoveModifiersFromSource(parent);
                    ((ShootingTower)tower).BulletSpeed.RemoveModifiersFromSource(parent);
                    ((ShootingTower)tower).Power.RemoveModifiersFromSource(parent);
                }
            }
            else if(tower is LaserTower)
            {
                if (!didRemove)
                {
                    didRemove = ((LaserTower)tower).DPS.RemoveModifiersFromSource(parent);
                }
                else
                {
                    ((LaserTower)tower).DPS.RemoveModifiersFromSource(parent);
                }
            }

            return didRemove;
        }

        private void OnEnable()
        {
            if (effectRangeMods == null || effectRangeMods.Length == 0) { effectRangeMods = new StatModifier[1]; }
            if (firingRateMods == null || firingRateMods.Length == 0) { firingRateMods = new StatModifier[1]; }
            if (bulletSpeedMods == null || bulletSpeedMods.Length == 0) { bulletSpeedMods = new StatModifier[1]; }
            if (powerMods == null || powerMods.Length == 0) { powerMods = new StatModifier[1]; }
        }
    }
}
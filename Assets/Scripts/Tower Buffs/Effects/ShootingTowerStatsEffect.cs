using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
namespace TowerBuffs
{
    [CreateAssetMenu(menuName = "Tower Buff Effect/Shooting Tower")]
    public class ShootingTowerStatsEffect : TowerBuffEffect
    {
        [SerializeField] protected StatModifier[] effectRangeMods = null;
        [SerializeField] protected StatModifier[] firingRateMods = null;
        [SerializeField] protected StatModifier[] bulletSpeedMods = null;
        [SerializeField] protected StatModifier[] powerBonuses = null;

        public override void ApplyEffect(BaseTowerBuff parent, Tower tower)
        {
            foreach (var mod in effectRangeMods)
            {
                var newMod = new StatModifier(mod, parent);
                tower.EffectRange.AddModifier(newMod);
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
                foreach (var mod in powerBonuses)
                {
                    var newMod = new StatModifier(mod, parent);
                    ((ShootingTower)tower).Power.AddModifier(newMod);
                }
            }
        }

        public override bool RemoveEffect(BaseTowerBuff parent, Tower tower)
        {
            bool didRemove = false;
            didRemove = tower.EffectRange.RemoveModifiersFromSource(parent);
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

            return didRemove;
        }

        private void OnEnable()
        {
            if (effectRangeMods == null || effectRangeMods.Length == 0) { effectRangeMods = new StatModifier[1]; }
            if (firingRateMods == null || firingRateMods.Length == 0) { firingRateMods = new StatModifier[1]; }
            if (bulletSpeedMods == null || bulletSpeedMods.Length == 0) { bulletSpeedMods = new StatModifier[1]; }
            if (powerBonuses == null || powerBonuses.Length == 0) { powerBonuses = new StatModifier[1]; }
        }
    }
}
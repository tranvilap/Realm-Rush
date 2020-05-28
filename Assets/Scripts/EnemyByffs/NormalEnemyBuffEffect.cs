using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Buffs
{
    [CreateAssetMenu(menuName = "Enemy Buff Effect/Normal Enemy")]
    public class NormalEnemyBuffEffect : EnemyBuffEffect
    {
        //[SerializeField] protected StatModifier[] maxHPMod = null;
        [SerializeField] protected StatModifier[] speedMod = null;

        public override void ApplyEffect(object source, Enemy enemy)
        {
            //foreach (var mod in maxHPMod)
            //{
            //    var newMod = new StatModifier(mod, source);
            //    enemy.MaxHP.AddModifier(mod);
            //}
            foreach (var mod in speedMod)
            {
                var newMod = new StatModifier(mod, source);
                enemy.MoveSpeed.AddModifier(newMod);
            }
        }

        public override bool RemoveEffect(object source, Enemy enemy)
        {
            //bool result = enemy.MaxHP.RemoveModifiersFromSource(source);
            //if (result)
            //{
            //    result = enemy.MoveSpeed.RemoveModifiersFromSource(source);
            //}
            //else
            //{
            //    enemy.MoveSpeed.RemoveModifiersFromSource(source);
            //}
            return enemy.MoveSpeed.RemoveModifiersFromSource(source);
        }
    }
}

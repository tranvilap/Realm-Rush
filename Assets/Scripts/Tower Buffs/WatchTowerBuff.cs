using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerBuffs
{
    public class WatchTowerBuff : InstantBuff
    {
        GameObject buffAura;
        public override void ApplyBuffsToTarget()
        {
            base.ApplyBuffsToTarget();
            var aura = SharedObjectPooler.main.GetPooledObject(Constants.WATCH_TOWER_BUFF_AURA);
            if(aura != null)
            {
                buffAura = aura;
                buffAura.SetActive(true);
                buffAura.transform.position = targetTower.transform.position;
            }
        }

        public override bool RemoveTargetBuffs()
        {
            bool result=  base.RemoveTargetBuffs();
            if (buffAura != null)
            {
                buffAura.SetActive(false);
            }
            return result;
        }
    }
}

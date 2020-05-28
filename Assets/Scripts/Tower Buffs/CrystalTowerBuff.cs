using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buffs
{
    public class CrystalTowerBuff : BaseEnemyBuff
    {
        GameObject buffVFX = null;
        // Start is called before the first frame update
        void Start()
        {
            ApplyBuffsToTarget();
        }
        private void Update()
        {
            if(buffVFX != null)
            {
                buffVFX.transform.position = Target.transform.position;
            }
        }

        public override void ApplyBuffsToTarget()
        {
            base.ApplyBuffsToTarget();

            buffVFX= SharedObjectPooler.main.GetPooledObject(Constants.SLOW_VFX);
            buffVFX.SetActive(true);
            buffVFX.transform.position = Target.transform.position;
        }
        public override void RemoveTargetBuffs()
        {
            if (!IsActivate) { return; }
            if(Source != null)
            {
                if(Source is CrystalTower)
                {
                    ((CrystalTower)Source).CancleBuff(this);
                }
            }
            base.RemoveTargetBuffs();
            if(buffVFX != null)
            {
                buffVFX.SetActive(false);
                buffVFX = null;
            }
            Destroy(gameObject);

        }
    } 
}

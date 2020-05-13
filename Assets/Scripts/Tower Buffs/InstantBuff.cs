using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerBuffs
{
    public class InstantBuff : BaseTowerBuff
    {
        protected BuffingTower sourceTower = null;

        public BuffingTower SourceTower { get => sourceTower; }

        public virtual void SetSourceTower(BuffingTower buffingTower)
        {
            sourceTower = buffingTower;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using Buffs;
using UnityEngine;

public class CrystalTower : UpgradeableTower
{
    [Header("Level 0")]
    [SerializeField] GameObject level0Model=null;
    [SerializeField] float level0RangeRadius = 1.5f;
    [SerializeField] BaseEnemyBuff level0Debuff = null;
    [Header("Level 1")]
    [SerializeField] GameObject level1Model=null;
    [SerializeField] float level1RangeRadius = 2f;
    [SerializeField] BaseEnemyBuff level1Debuff = null;
    [Header("Level 2")]
    [SerializeField] GameObject level2Model=null;
    [SerializeField] float level2RangeRadius = 2.5f;
    [SerializeField] BaseEnemyBuff level2Debuff = null;


    List<BaseEnemyBuff> givingBuffs = new List<BaseEnemyBuff>();
    BaseEnemyBuff currentDebuff;

    private void Update()
    {
        SeekTarget();
    }

    protected override void SetUpTower()
    {
        switch(CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    level0Model.SetActive(true);
                    level1Model.SetActive(false);
                    level2Model.SetActive(false);
                    EffectRangeRadius.BaseValue = level0RangeRadius;
                    currentDebuff = level0Debuff;
                    
                    break;
                }
            case 1:
                {
                    level0Model.SetActive(false);
                    level1Model.SetActive(true);
                    level2Model.SetActive(false);
                    EffectRangeRadius.BaseValue = level1RangeRadius;
                    currentDebuff = level1Debuff;
                    break;
                }
            case 2:
            default:
                {
                    level0Model.SetActive(false);
                    level1Model.SetActive(false);
                    level2Model.SetActive(true);
                    EffectRangeRadius.BaseValue = level2RangeRadius;
                    currentDebuff = level2Debuff;
                    break;
                }
        }
    }
    protected override void SeekTarget()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        List<Enemy> detectedEnemies = new List<Enemy>();
        foreach (var col in hitColliders)
        {
            var enemyScript = col.GetComponent<Enemy>();
            if (enemyScript == null) { continue; }
            if (enemyScript.isDead || enemyScript.reachedGoal) { continue; }
            detectedEnemies.Add(enemyScript);
            bool isHavingSameBuff = false;

            for (int i = enemyScript.Buffs.Count - 1; i>=0; i--)
            {
                var buff = enemyScript.Buffs[i];
                if (buff.buffType == currentDebuff.buffType)
                {
                    if (buff.BuffLevel >= CurrentTowerUpgradeLevel)
                    {
                        isHavingSameBuff = true;
                    }
                    else
                    {
                        enemyScript.Buffs.Remove(buff);
                        buff.RemoveTargetBuffs();
                    }
                }
            }
            if (!isHavingSameBuff)
            {
                var newBuff = Instantiate(currentDebuff, enemyScript.transform);
                newBuff.Init(enemyScript, CurrentTowerUpgradeLevel, this);
                givingBuffs.Add(newBuff);
            }
        }
        CheckGivingBuffs(detectedEnemies);
    }
    private void CheckGivingBuffs(List<Enemy> detectedEnemies)
    {        
        for (int i = givingBuffs.Count - 1; i >= 0; i--)
        {
            var buff = givingBuffs[i];
            if (!detectedEnemies.Contains(buff.Target))
            {
                if (buff != null)
                {
                    buff.Target.Buffs.Remove(buff);
                    buff.RemoveTargetBuffs();
                }
            }
        }
    }
    public void CancleBuff(BaseEnemyBuff baseEnemyBuff)
    {
        givingBuffs.Remove(baseEnemyBuff);
    }


}

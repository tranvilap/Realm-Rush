using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeDisplayer : MonoBehaviour
{
    Tower choosingTower = null;

    public Tower ChoosingTower { get => choosingTower; }
    public bool IsDisplaying { get { return (choosingTower != null); } }
    public void ShowRange(Tower tower)
    {

    }
    public void HideRange()
    {
        gameObject.SetActive(false);
        choosingTower = null;
    }
    public void UpdateRange()
    {
        if (choosingTower == null) { return; }
        transform.position = choosingTower.transform.position;
        float rangeDiameter = choosingTower.EffectRangeRadius.CalculatedValue * 2;
        transform.localScale = new Vector3(rangeDiameter, rangeDiameter, rangeDiameter);
    }
}

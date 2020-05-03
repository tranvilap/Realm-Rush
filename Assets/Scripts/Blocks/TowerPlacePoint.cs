using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacePoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    [SerializeField] GameObject pedestal = null;
    Tower placingTower = null;
    GameObject previewingTower = null;

    public bool IsPlaceable
    {
        get => isPlaceable;
        set
        {
            isPlaceable = value;
            pedestal.SetActive(isPlaceable);
        }
    }

    public void ShowPreviewTower(GameObject previewTower)
    {
        previewingTower = Instantiate(previewTower, transform.position, Quaternion.identity);
    }

    public void HidePreviewTower()
    {
        if (previewingTower == null) { return; }

        Destroy(previewingTower.gameObject);
        previewingTower = null;
    }

    public void BuildTower(GameObject tower)
    {
        if (!isPlaceable) { return; }
        HidePreviewTower();
        var go = Instantiate(tower, transform.position, Quaternion.identity);
        placingTower = go.GetComponent<Tower>();
        if (placingTower != null)
        {
            placingTower.PlaceTowerAt(this);
        }
        IsPlaceable = false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerPoint : MonoBehaviour
{
    [SerializeField] bool isPlacable = true;
    GameObject placingTower = null;
    GameObject previewingTower = null;

    public bool IsPlacable { get => isPlacable; set => isPlacable = value; }

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

    public void PlaceTower(GameObject tower)
    {
        HidePreviewTower();
        placingTower = Instantiate(tower, transform.position, Quaternion.identity);
        isPlacable = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerController : MonoBehaviour
{
    [SerializeField] GameObject towerPreviewPrefab;
    [SerializeField] GameObject towerPrefab;

    private SpawnPoint choosingSpawpoint = null;

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selection = hit.transform;
            var spawnPoint = selection.GetComponent<SpawnPoint>();
            if (spawnPoint != null)
            {
                if (spawnPoint.IsPlacable)
                {
                    if (spawnPoint != choosingSpawpoint)
                    {
                        if (choosingSpawpoint != null)
                        {
                            choosingSpawpoint.HidePreviewTower();
                        }
                        choosingSpawpoint = spawnPoint;
                        choosingSpawpoint.ShowPreviewTower(towerPreviewPrefab);
                    }
                    if (Input.GetMouseButton(0))
                    {
                        spawnPoint.PlaceTower(towerPrefab);
                    }
                }
            }
            else
            {
                if (choosingSpawpoint != null)
                {
                    choosingSpawpoint.HidePreviewTower();
                }
            }
        }
    }
}

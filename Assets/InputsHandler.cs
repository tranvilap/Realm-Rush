using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsHandler : MonoBehaviour
{
    PlaceTowerController placeTowerController = null;
    // Start is called before the first frame update
    void Start()
    {
        placeTowerController = FindObjectOfType<PlaceTowerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        //Handle placing tower
        if (placeTowerController.ChoosingTowerData == null) { return; } //Todo Check if select exist tower to open object
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selection = hit.transform;
            var spawnPoint = selection.GetComponent<TowerPlacePoint>();
            placeTowerController.CheckTowerPlaceable(spawnPoint);
        }
    }
}

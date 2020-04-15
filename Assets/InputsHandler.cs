using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsHandler : MonoBehaviour
{
    PlaceTowerController placeTowerController = null;

    Canvas showingTowerMenu = null;
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
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selection = hit.transform;

            var tower = selection.GetComponent<Tower>(); //Handle Tower Menu Open/Close
            if (tower != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (showingTowerMenu != tower.MenuCanvas)
                    {
                        if (showingTowerMenu != null)
                        {
                            showingTowerMenu.gameObject.SetActive(false);
                        }
                        tower.OpenTowerMenu();
                        showingTowerMenu = tower.MenuCanvas;
                    }
                    else
                    {
                        if (!showingTowerMenu.gameObject.activeInHierarchy)
                        {
                            showingTowerMenu.gameObject.SetActive(true);
                        }
                    }
                }
                return;
            }
            //Close Menu canvas if player press somewhere different from menu
            if (showingTowerMenu != null 
                && Input.GetMouseButtonUp(0)) { showingTowerMenu.gameObject.SetActive(false); return; } 

            var towerPlacePoint = selection.GetComponent<TowerPlacePoint>();
            if (towerPlacePoint != null)
            {
                if (placeTowerController.ChoosingTowerData != null)
                {
                    placeTowerController.CheckTowerPlaceable(towerPlacePoint);
                    return;
                }
            }
            //Destroy Preview Tower left in the screen
            placeTowerController.CeasePlacingTower();


        }
    }
}

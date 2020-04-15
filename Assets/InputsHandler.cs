using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsHandler : MonoBehaviour
{
    PlaceTowerController placeTowerController = null;

    Canvas showingTowerMenu = null; 
    Canvas currentPointingPlaceTowerPoint = null;
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

            //Handle Hover Mouse

            //Handle Placing Tower 
            var towerPlacePoint = selection.GetComponent<TowerPlacePoint>();
            if (towerPlacePoint != null)
            {
                if (placeTowerController.ChoosingTowerData != null)
                {
                    placeTowerController.CheckTowerPlaceable(towerPlacePoint);
                }
            }
            else
            {
                //Destroy Preview Tower left in the screen
                placeTowerController.CeasePlacingTower();
            }

            //Handle Clicked Mouse 0
            if (Input.GetMouseButtonUp(0))
            {
                //Handle Opening/Closing Tower Menu
                var tower = selection.GetComponent<Tower>(); 
                if (tower != null)
                {
                    if (showingTowerMenu != null)
                    {
                        showingTowerMenu.gameObject.SetActive(false);
                    }
                    tower.OpenTowerMenu();
                    showingTowerMenu = tower.MenuCanvas;
                    return;
                }

                if(showingTowerMenu != null)
                {
                    if(showingTowerMenu.gameObject.activeInHierarchy)
                    {
                        showingTowerMenu.gameObject.SetActive(false);
                        showingTowerMenu = null;
                        return;
                    }
                    else
                    {
                        showingTowerMenu = null;
                    }
                }

                //Handle Placing Tower
                placeTowerController.PlaceChoosingTower();

            }
        }
    }
}

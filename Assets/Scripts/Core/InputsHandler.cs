using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class InputsHandler : MonoBehaviour
{
    PlaceTowerController placeTowerController = null;
    Tower showingMenuTower = null;
    Bounds cameraBounds;
    CameraController cameraController;
    TowerEvents.TowerEvents towerEvents;

    // Start is called before the first frame update
    void Start()
    {
        placeTowerController = FindObjectOfType<PlaceTowerController>();
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Be careful about || EventSystem.current.currentSelectedGameObject != null
        if (EventSystem.current.IsPointerOverGameObject() || cameraController.cameraIsMoving)
        {
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selection = hit.transform;

            //Handle Hover Mouse
            PreviewTowerOnPlacePoint(selection);

        }
        //Handle Clicked Mouse 0
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.currentSelectedGameObject != null) { return; }
            //Handle Opening/Closing Tower Menu
            var tower = hit.transform.GetComponent<Tower>();
            if (tower != null)
            {
                OpenTowerMenuAndShowTowerRange(tower);
                return;
            }

            if (showingMenuTower != null)
            {
                if (showingMenuTower.MenuCanvas.gameObject.activeInHierarchy)
                {
                    CloseTowerMenuAndTowerRange();
                    return;
                }
                else
                {
                    showingMenuTower = null;
                }
            }

            //Handle Placing Tower
            placeTowerController.PlaceChoosingTower();

        }
    }




    private void CloseTowerMenuAndTowerRange()
    {
        showingMenuTower.CloseTowerMenu();
        showingMenuTower = null;
    }

    private void OpenTowerMenuAndShowTowerRange(Tower tower)
    {
        if (showingMenuTower != null)
        {
            showingMenuTower.CloseTowerMenu() ;
        }
        tower.OpenTowerMenu();
        showingMenuTower = tower;

    }


    private void PreviewTowerOnPlacePoint(Transform selection)
    {
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
    }

}

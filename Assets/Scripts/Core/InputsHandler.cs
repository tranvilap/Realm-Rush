using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class InputsHandler : MonoBehaviour
{
    [SerializeField] TowerRangeDisplayer towerRangeDisplayer = null;

    PlaceTowerController placeTowerController = null;
    Canvas showingTowerMenu = null;
    Bounds cameraBounds;
    CameraController cameraController;

    private void OnEnable()
    {
        if(placeTowerController == null)
        {
            placeTowerController = FindObjectOfType<PlaceTowerController>();
        }
        placeTowerController.OnUpgradingTowerEvent += OnUpgradingTower;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Be careful about || EventSystem.current.currentSelectedGameObject != null
        if (EventSystem.current.IsPointerOverGameObject() || cameraController.cameraIsMoving )
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
            if(EventSystem.current.currentSelectedGameObject != null) {return; }
            //Handle Opening/Closing Tower Menu
            var tower = hit.transform.GetComponent<Tower>();
            if (tower != null)
            {
                OpenTowerMenuAndShowTowerRange(tower);
                return;
            }

            if (showingTowerMenu != null)
            {
                if (showingTowerMenu.gameObject.activeInHierarchy)
                {
                    CloseTowerMenuAndTowerRange();
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




    private void CloseTowerMenuAndTowerRange()
    {
        showingTowerMenu.gameObject.SetActive(false);
        showingTowerMenu = null;
        towerRangeDisplayer.HideRange();
    }

    private void OpenTowerMenuAndShowTowerRange(Tower tower)
    {
        if (showingTowerMenu != null)
        {
            showingTowerMenu.gameObject.SetActive(false);
        }
        tower.OpenTowerMenu();
        showingTowerMenu = tower.MenuCanvas;

        towerRangeDisplayer.ShowRange(tower);
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

    void OnUpgradingTower(UpgradeableTower tower)
    {
        towerRangeDisplayer.UpdateRange();
    }

    private void OnDisable()
    {
        placeTowerController.OnUpgradingTowerEvent -= OnUpgradingTower;
    }
}

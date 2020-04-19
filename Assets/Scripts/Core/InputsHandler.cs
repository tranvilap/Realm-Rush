using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputsHandler : MonoBehaviour
{
    [SerializeField] [Tooltip("Main Camera for controlling moving around")] Camera mainCamera = null;
    [SerializeField] float cameraDragSpeed = 30f;
    [SerializeField] [Tooltip("Area represents moving range of camera")]BoxCollider cameraAreaBox = null;

    [HideInInspector]public bool isDragingCamera = false;

    PlaceTowerController placeTowerController = null;
    Canvas showingTowerMenu = null;
    Bounds cameraBounds;

    // Start is called before the first frame update
    void Start()
    {
        placeTowerController = FindObjectOfType<PlaceTowerController>();
        cameraBounds = cameraAreaBox.bounds;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            ExitDragCamera();
            return;
        }

        if (Input.GetMouseButton(0)) //Enter Drag Camera
        {
            DragCamera();
        }
        if (isDragingCamera) //Exit draging camera
        {
            ExitDragCamera();
            return;
        }


        //Handle placing tower
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var selection = hit.transform;

            //Handle Hover Mouse
            PreviewTowerOnPlacePoint(selection);

            //Handle Clicked Mouse 0
            if (Input.GetMouseButtonUp(0))
            {
                //Handle Opening/Closing Tower Menu
                var tower = selection.GetComponent<Tower>();
                if (tower != null)
                {
                    OpenTowerMenu(tower);
                    return;
                }

                if (showingTowerMenu != null)
                {
                    if (showingTowerMenu.gameObject.activeInHierarchy)
                    {
                        CloseTowerMenu();
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

    private void CloseTowerMenu()
    {
        showingTowerMenu.gameObject.SetActive(false);
        showingTowerMenu = null;
    }

    private void OpenTowerMenu(Tower tower)
    {
        if (showingTowerMenu != null)
        {
            showingTowerMenu.gameObject.SetActive(false);
        }
        tower.OpenTowerMenu();
        showingTowerMenu = tower.MenuCanvas;
    }

    private void ExitDragCamera()
    {
        if (isDragingCamera)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDragingCamera = false;
            }
        }
    }

    private void DragCamera()
    {
        float xAxis = Input.GetAxis("Mouse X");
        float yAxis = Input.GetAxis("Mouse Y");
        if (!Mathf.Approximately(xAxis, 0f) || !Mathf.Approximately(yAxis, 0f))
        {
            isDragingCamera = true;
            float speed = cameraDragSpeed * Time.deltaTime;
            var currentPos = mainCamera.transform.position;
            mainCamera.transform.position = new Vector3(Mathf.Clamp(currentPos.x + xAxis * speed, cameraBounds.min.x, cameraBounds.max.x)
                , currentPos.y + 0.0f,
                Mathf.Clamp(currentPos.z + yAxis * speed, cameraBounds.min.z, cameraBounds.max.z));
        }
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

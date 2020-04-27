using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour
{
    Camera mainCamera;
    Camera MainCamera
    {
        get
        {
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            return mainCamera;
        }
    }
    CameraController cameraController;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if(cameraController==null)
        {
            cameraController= FindObjectOfType<CameraController>();
        }
        cameraController.RotateCameraEvent += OnRotateCamera;
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 v = MainCamera.transform.position - transform.position;

        v.x = v.z = 0.0f;

        transform.LookAt(MainCamera.transform.position - v);

        transform.rotation = (MainCamera.transform.rotation);
    }

    public void OnRotateCamera(Transform cameraTransform)
    {
        LookAtCamera();
    }
    private void OnDisable()
    {
        cameraController.RotateCameraEvent -= OnRotateCamera;
    }
}

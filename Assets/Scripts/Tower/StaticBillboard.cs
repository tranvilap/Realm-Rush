using UnityEngine;

public class StaticBillboard : MonoBehaviour
{
    Camera mainCamera;
    Camera MainCamera
    {
        get
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
            return mainCamera;
        }
    }
    CameraController cameraController;

    protected virtual void OnEnable()
    {
        if (cameraController == null)
        {
            cameraController = FindObjectOfType<CameraController>();
        }
        cameraController.RotateCameraEvent += OnRotateCamera;
        LookAtCamera();
    }

    private void Start()
    {
        LookAtCamera();
    }

    protected virtual void LookAtCamera()
    {
        Vector3 v = MainCamera.transform.position - transform.position;

        v.x = v.z = 0.0f;

        transform.LookAt(MainCamera.transform.position - v);

        transform.rotation = (MainCamera.transform.rotation);
    }

    protected virtual void OnRotateCamera(Transform cameraTransform)
    {
        LookAtCamera();
    }
    protected virtual void OnDisable()
    {
        cameraController.RotateCameraEvent -= OnRotateCamera;
    }
}

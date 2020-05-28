using UnityEngine;
using UnityEngine.EventSystems;
class CameraController : MonoBehaviour
{
    [SerializeField] BoxCollider cameraBorder = null;
    [SerializeField] float smoothFactor = 5f;
    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float clampFloorAngle = -80f;
    [SerializeField] float clampCeilAngle = 80f;
    [Header("Zoom")]
    [SerializeField] Vector3 zoomAmount = new Vector3(0f, -10f, 10f);

    Vector3 newPos;
    Vector3 currentEulerAngle;
    Vector3 newZoom;

    Vector3 dragStartPos;
    Vector3 dragCurerntPos;

    float rotX = 0f;
    float rotY = 0f;

    Camera mainCamera;
    Bounds borderBounds;
    public bool cameraIsMoving = false;
    public delegate void OnRotateCamera(Transform transform);
    public event OnRotateCamera RotateCameraEvent;

    
    private void Start()
    {
        mainCamera = Camera.main;
        borderBounds = cameraBorder.bounds;

        newPos = transform.position;
        ClampNewPostition();

        currentEulerAngle = transform.rotation.eulerAngles;
        rotX = currentEulerAngle.x;
        rotY = currentEulerAngle.y;

        newZoom = mainCamera.transform.localPosition;
        ClampNewZoom();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            ClampNewZoom();
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject != null) { return; }
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragStartPos = ray.GetPoint(entry);
            }

        }
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.currentSelectedGameObject !=null) { return; }
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 tmp = newPos;
            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurerntPos = ray.GetPoint(entry);
                newPos = transform.position + dragStartPos - dragCurerntPos;
                ClampNewPostition();
            }
            if (newPos == tmp)
            {
                cameraIsMoving = false;
            }
            else
            {
                cameraIsMoving = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            cameraIsMoving = false;
        }


        if (Input.GetMouseButton(1))
        {
            float cache = rotY;
            rotY += Input.GetAxis("Rotate Camera X") * rotationSpeed;
            rotX -= Input.GetAxis("Rotate Camera Y") * rotationSpeed;

            rotX = Mathf.Clamp(rotX, clampFloorAngle, clampCeilAngle);
        }
    }

    private void ClampNewZoom()
    {
        newZoom.y = Mathf.Clamp(newZoom.y, borderBounds.min.y, borderBounds.max.y);
        newZoom.z = Mathf.Clamp(newZoom.z, -borderBounds.max.y, -borderBounds.min.y);
    }

    private void ClampNewPostition()
    {
        newPos.x = Mathf.Clamp(newPos.x, borderBounds.min.x, borderBounds.max.x);
        newPos.z = Mathf.Clamp(newPos.z, borderBounds.min.z, borderBounds.max.z);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoothFactor);

        currentEulerAngle = Vector3.Lerp(currentEulerAngle, new Vector3(rotX, rotY, 0.0f), Time.deltaTime * rotationSpeed);
        if(transform.rotation != Quaternion.Euler(currentEulerAngle.x, currentEulerAngle.y, 0.0f))
        {

            transform.rotation = Quaternion.Euler(currentEulerAngle.x, currentEulerAngle.y, 0.0f);
            RotateCameraEvent?.Invoke(mainCamera.transform);
        }

        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, newZoom, Time.deltaTime * smoothFactor);

    }


}
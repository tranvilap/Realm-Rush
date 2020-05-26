using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBillboard : MonoBehaviour
{
    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}

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
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - MainCamera.transform.position);
        Vector3 v = MainCamera.transform.position - transform.position;

        v.x = v.z = 0.0f;

        transform.LookAt(MainCamera.transform.position - v);

        transform.rotation = (MainCamera.transform.rotation);
    }
}

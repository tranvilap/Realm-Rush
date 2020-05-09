using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrystalRotation : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 30f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotateSpeed*Time.deltaTime, 0f);
    }
}

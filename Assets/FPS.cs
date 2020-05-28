using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPS : MonoBehaviour
{
    Text text;
    [SerializeField] Text resolution;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        resolution.text = (Screen.currentResolution.refreshRate).ToString();
        Application.targetFrameRate = 60;

    }

    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        text.text = (1/deltaTime).ToString();
    }

}
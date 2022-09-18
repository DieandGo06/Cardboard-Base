using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FPSCounter : MonoBehaviour
{
    TextMeshProUGUI fpsDisplay;



    private void Start()
    {
        fpsDisplay = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + fps + " fps";
    }
}

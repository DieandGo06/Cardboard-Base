using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugRotation : MonoBehaviour
{
    TextMeshProUGUI texto;
    public GameObject cam;
    public bool showAngle;
    public bool showCamMoved;

    Quaternion startRotation;



    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        startRotation = cam.transform.rotation;
        
    }

    void Update()
    {
        //Debug.Log(cam);
        if (showAngle)
        {
            var prueba = cam.transform.rotation.eulerAngles;
            //texto.text = cam.transform.rotation.ToString();
            texto.text = prueba.ToString();

        }
        if (showCamMoved) 
        {
            if (cam.transform.rotation != startRotation) texto.text = "true";
        }
    }
}

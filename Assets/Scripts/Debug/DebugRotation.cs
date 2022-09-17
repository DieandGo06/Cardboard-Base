using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugRotation : MonoBehaviour
{
    TextMeshProUGUI texto;
    public GameObject cam;
    public GameObject player;
    public bool showCamAngle;
    public bool showCamMoved;
    public bool showPlayerAngle;

    Quaternion startRotation;



    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        startRotation = cam.transform.rotation;
        
    }

    void Update()
    {
        //Debug.Log(cam);
        if (showCamAngle)
        {
            var prueba = cam.transform.localRotation.eulerAngles;
            texto.text = prueba.ToString();

            //var prueba = cam.transform.forward;
            //texto.text = cam.transform.rotation.ToString();
        }
        if (showPlayerAngle)
        {
            var prueba2 = player.transform.rotation.eulerAngles;
            texto.text = prueba2.ToString();
        }
        if (showCamMoved) 
        {
            if (cam.transform.rotation != startRotation) texto.text = "true";
        }
    }
}

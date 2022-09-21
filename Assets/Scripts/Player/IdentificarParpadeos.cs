using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentificarParpadeos : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.instance != null) GameManager.instance.parpadeoObject = this.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

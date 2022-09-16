using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatosDelChanguito : MonoBehaviour
{
    // Start is called before the first frame update
    public int perdida;
    [Range (0,10)]public int cuantoDineroGasto;
    [Range(0, 10)] public int cuantaVidaGasto;
    public Slider barraDinero;
    public Slider barraVida;

    private void Update()
    {
        barraDinero.value = cuantoDineroGasto;
        barraVida.value = cuantaVidaGasto;
    }

}

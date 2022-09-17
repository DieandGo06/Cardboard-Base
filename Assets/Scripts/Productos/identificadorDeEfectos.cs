using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //IMPORTANTE!!!!
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class identificadorDeEfectos : MonoBehaviour
{
    //public bool esSaludable;

    //public bool seEjecutaUnaConstante;

    //bool veMal = true;


    //Para el temporizador de las paredes

    //float tiempoTranscurrido;
    //bool apareciendoSucia;
    //bool apareciendoLimpia;

    [SerializeField] float speed;
     bool seEjecutaUnaVez;

    //Para el caminante
    public Caminante caminante;
    public string nombreDeProducto;
    bool choca;
    bool aumentar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!seEjecutaUnaConstante) //PAREDES-------------------------------------------------------------------------
        //{

        //    if (tiempoTranscurrido < 1 && apareciendoSucia == true || tiempoTranscurrido < 1 && apareciendoLimpia == true)
        //    {
        //        if (apareciendoSucia == true)
        //        {
        //            //tiene que desaparecr el otro
        //            GameManager.instance.paredes.transform.GetChild(1).gameObject.SetActive(false);

        //            AparecerParedSucia();
        //        }
        //        if (apareciendoLimpia == true)
        //        {
        //            //tiene que desaparecr el otro
        //            GameManager.instance.paredes.transform.GetChild(0).gameObject.SetActive(false);

        //            //AparecerParedLimpia();
        //            AparecerParedLimpia();
        //        }
        //    }
        //    else
        //    {

        //        seEjecutaUnaConstante = false;
        //    }
        //}


        if (!seEjecutaUnaVez)
        {

            if (choca) {
                if (nombreDeProducto != null)
                {
                    aumentar = true;
                }

                if (nombreDeProducto == "bebida")
                {
                    GameManager.instance.contadorBebida--;
                    Lista(GameManager.instance.contadorBebida);
                }

                if (nombreDeProducto == "galletitas")
                {
                    GameManager.instance.contadorGalletitas--;
                    Lista(GameManager.instance.contadorGalletitas);
                }

                seEjecutaUnaVez = true;

            }
        }

        //Debug.Log(nombreDeProducto);


        //if (transform.parent != null)
        //{
        //    if (transform.parent.tag == "CarritoSpace")
        //    {
        //        seEjecutaUnaVez = true;

        //if (nombreDeProducto == "galletitas")
        //{
        //    //ACA LE DETERMINO EN QUE SEGUNDO LUEGO DE TOMAR EL EFECTO TIENE QUE PRENDERSE Y APAGARSE LA LOooz!!
        //    Tareas.Nueva(0.5f, GlitchOff);
        //    Tareas.Nueva(0.7f, GlitchOn);
        //    Tareas.Nueva(0.9f, GlitchOff);
        //    Tareas.Nueva(0.99f, GlitchOn);
        //    Tareas.Nueva(1.2f, GlitchOff);
        //    Tareas.Nueva(1.29f, GlitchOn);
        //    Tareas.Nueva(1.4f, GlitchOff);
        //    Tareas.Nueva(1.5f, GlitchOn);
        //    Tareas.Nueva(1.8f, GlitchOff);
        //    Tareas.Nueva(1.95f, GlitchOn);
        //    Tareas.Nueva(2.0f, GlitchOff);
        //    Tareas.Nueva(2.09f, GlitchOn);
        //    Tareas.Nueva(2.0f, GlitchOff);

        //}

        //if (nombreDeProducto == "vision")
        //{
        //    veMal = false;

        //}

        //if (nombreDeProducto == "coquita")
        //{
        //    AcelerarCarrito();
        //    Tareas.Nueva(5, RalentizarCarrito);
        //}

        //if (nombreDeProducto == "agua") //El agua mejora el estado de las paredes
        //{
        //    apareciendoLimpia = true;

        //    GameManager.instance.paredes.transform.GetChild(2).gameObject.SetActive(true);


        //}



    }

    private void OnTriggerEnter(Collider other)
    {
        ActivarCaminante();
    }


    void Lista (float contador ) {

        if (contador == 0)
        {
            caminante.seMueve = true;
        }


        Debug.Log(contador);

    }
    void ActivarCaminante() {
        choca = true;
    }


    ////Blurr ------------------------------------------
    //if (!veMal && GameManager.instance.playerInfo.blur < 20)
    //{
    //    tiempoTranscurrido += Time.deltaTime;
    //    MejorarVision();
    //}

    //if (tiempoTranscurrido > 2) {
    //    veMal = true;
    //}



    //void MejorarVision()
    //{
    //    GameManager.instance.playerInfo.blur += Time.deltaTime * 0.5f;
    //    float _blur = GameManager.instance.playerInfo.blur;
    //    GameManager.instance.playerInfo.SetBlurValue(_blur);
    //    Debug.Log(_blur);
    //}


    ////powerUp carrito ------------------------------------------
    //void RalentizarCarrito()
    //{
    //    GameManager.instance.jugador.GetComponent<PlayerMovement>().speed = 2;
    //}

    //void AcelerarCarrito()
    //{
    //    GameManager.instance.jugador.GetComponent<PlayerMovement>().speed = 20;
    //}

    ////Luces -----------------------------------------------------------------------------------------
    //void GlitchOff() //ESTA ES LA PANTALLA EN NEGRO------------------------------------------------
    //{

    //    float alpha = Random.Range(0.85f, 0.95f);
    //    GameManager.instance.corteDeLuz.GetComponent<RawImage>().color = new Color(0, 0, 0, alpha);

    //}
    //void GlitchOn() //ESTA ES LA PANTALLA TRANSLUCIDA
    //{

    //    GameManager.instance.corteDeLuz.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

    //}

    ////Paredes ------------------------------------------
    //void AparecerParedSucia()
    //{
    //    float segundos = 4;
    //    tiempoTranscurrido += Time.deltaTime / segundos;
    //    GameObject coso = GameManager.instance.paredes.transform.GetChild(0).gameObject;
    //    coso.GetComponent<Renderer>().material.color = new Color(1, 1, 1, tiempoTranscurrido);


    //}

    //void AparecerParedLimpia()
    //{
    //    float segundos = 4;
    //    tiempoTranscurrido += Time.deltaTime / segundos;
    //    GameObject coso = GameManager.instance.paredes.transform.GetChild(2).gameObject;
    //    coso.GetComponent<Renderer>().material.color = new Color(1, 1, 1, tiempoTranscurrido);


    //}


}
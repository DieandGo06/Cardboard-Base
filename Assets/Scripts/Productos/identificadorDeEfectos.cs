using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //IMPORTANTE!!!!
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class identificadorDeEfectos : MonoBehaviour
{
    public bool esSaludable;
    GameManager gm;

    //public bool seEjecutaUnaConstante;

    //bool veMal = true;


    //Para el temporizador de las paredes

    //float tiempoTranscurrido;
    //bool apareciendoSucia;
    //bool apareciendoLimpia;

     bool seEjecutaUnaVez;

    //Para el caminante
    public Caminante caminante;
    public string nombreDeProducto;
    bool choca;
    bool efecto;


    void Start()
    {
        gm = GameManager.instance;
        choca = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gm.jugador.GetComponent<PlayerController>().speed);




        //if (!seEjecutaUnaConstante) //PAREDES-------------------------------------------------------------------------
        //{

        //    if (tiempoTranscurrido < 1 && apareciendoSucia == true || tiempoTranscurrido < 1 && apareciendoLimpia == true)
        //    {
        //        if (apareciendoSucia == true)
        //        {
        //            //tiene que desaparecr el otro
        //            gm.paredes.transform.GetChild(1).gameObject.SetActive(false);

        //            AparecerParedSucia();
        //        }
        //        if (apareciendoLimpia == true)
        //        {
        //            //tiene que desaparecr el otro
        //            gm.paredes.transform.GetChild(0).gameObject.SetActive(false);

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


                if (nombreDeProducto == "bebida")
                {
                    gm.contadorBebida--;
                    Lista(gm.contadorBebida);
                }

                if (nombreDeProducto == "galletitas")
                {
                    gm.contadorGalletitas--;
                    Lista(gm.contadorGalletitas);


                        if (!esSaludable)
                        {
                            RalentizarCarrito();
                        }
                        else if (esSaludable)
                        {
                        float speed = 40;
                        gm.jugador.GetComponent<PlayerController>().speed = speed;

                            //AcelerarCarrito();
                            Debug.Log("esto esta andando");
                        }

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

    

        //if (nombreDeProducto == "agua") //El agua mejora el estado de las paredes
        //{
        //    apareciendoLimpia = true;

        //    gm.paredes.transform.GetChild(2).gameObject.SetActive(true);


        //}



    }

    //--------------- CODIGO DE LISTA------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        ActivarCaminante();
    }


    void Lista (float contador ) {

        if (contador == 0)
        {
            caminante.seMueve = true;
          
        }


    }
    void ActivarCaminante() {
        choca = true;
    }

    //------------------------------------------------------------------

    ////Blurr ------------------------------------------
    //if (!veMal && gm.playerInfo.blur < 20)
    //{
    //    tiempoTranscurrido += Time.deltaTime;
    //    MejorarVision();
    //}

    //if (tiempoTranscurrido > 2) {
    //    veMal = true;
    //}



    //void MejorarVision()
    //{
    //    gm.playerInfo.blur += Time.deltaTime * 0.5f;
    //    float _blur = gm.playerInfo.blur;
    //    gm.playerInfo.SetBlurValue(_blur);
    //    Debug.Log(_blur);
    //}


    //powerUp carrito ------------------------------------------
    void RalentizarCarrito()
    {
        gm.jugador.GetComponent<PlayerController>().speed -= 3;
    
    }

    void AcelerarCarrito()
    {
        gm.jugador.GetComponent<PlayerController>().speed += 3;
    }

    ////Luces -----------------------------------------------------------------------------------------
    //void GlitchOff() //ESTA ES LA PANTALLA EN NEGRO------------------------------------------------
    //{

    //    float alpha = Random.Range(0.85f, 0.95f);
    //    gm.corteDeLuz.GetComponent<RawImage>().color = new Color(0, 0, 0, alpha);

    //}
    //void GlitchOn() //ESTA ES LA PANTALLA TRANSLUCIDA
    //{

    //    gm.corteDeLuz.GetComponent<RawImage>().color = new Color(0, 0, 0, 0);

    //}

    ////Paredes ------------------------------------------
    //void AparecerParedSucia()
    //{
    //    float segundos = 4;
    //    tiempoTranscurrido += Time.deltaTime / segundos;
    //    GameObject coso = gm.paredes.transform.GetChild(0).gameObject;
    //    coso.GetComponent<Renderer>().material.color = new Color(1, 1, 1, tiempoTranscurrido);


    //}

    //void AparecerParedLimpia()
    //{
    //    float segundos = 4;
    //    tiempoTranscurrido += Time.deltaTime / segundos;
    //    GameObject coso = gm.paredes.transform.GetChild(2).gameObject;
    //    coso.GetComponent<Renderer>().material.color = new Color(1, 1, 1, tiempoTranscurrido);


    //}


}

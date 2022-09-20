using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI; //IMPORTANTE!!!!
//using UnityEngine.Rendering;
//using UnityEngine.Rendering.Universal;

public class identificadorDeEfectos : MonoBehaviour
{
    public bool esSaludable;
    GameManager gm;

    
    //bool veMal = true;

    //Para parpadear
    float speed;
    bool abierto;
    int contador;
    bool seEjecutaUnaVez;

    //Para el caminante
    public string nombreDeProducto;
    bool choca;
    bool desactivarEfecto;


    //Para el pesta�eo
    public GameObject parpados;
    float posYparpadoUp;
    float posYparpadoInf;
    float tiempoTranscurrido;
    float blinkDuration;
    bool estaBlinkeando;
    int direccion;

    //Arritmias
    ActivaSonido arritmia;



    void Start()
    {
        gm = GameManager.instance;
        choca = false;


        if (parpados != null)
        {
            posYparpadoUp = parpados.transform.GetChild(0).transform.localPosition.y;
            posYparpadoInf = parpados.transform.GetChild(1).transform.localPosition.y;
        }



    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("contGolo" + gm.contadorGolosinas);
        //Debug.Log("contGalles" + gm.contadorGalletitas);
        //Debug.Log("contFruta" + gm.contadorFrutas);
        //Debug.Log("contCarne" + gm.contadorCarne);
        //Debug.Log("contCereales" + gm.contadorCereales);

        Debug.Log("esta true el blink" + estaBlinkeando);

       

        if (estaBlinkeando)
        {
            float speed = 600;
            posYparpadoUp -= Time.deltaTime * direccion * speed;
            posYparpadoInf += Time.deltaTime * direccion * speed;
            Tareas.Nueva(0.2f, CarritoFuerteClose);

        }

    }

    public void ReducirContadoryActivarEfectos()
    {
       


        if (!seEjecutaUnaVez)
        {

            if (nombreDeProducto == "frutas")
            {
                gm.contadorFrutas--;
            }

            if (nombreDeProducto == "verdura")
            {
                gm.contadorVerduras--;
            }

            if (nombreDeProducto == "carne")
                gm.contadorCarne--;
            {
                if (esSaludable && gm.contadorCarne <= 1)
                {
                    ActivarPestaneo();
                    gm.latidos.GetComponent<AudioSource>().volume = 0.2f;

                }

                if (esSaludable && gm.contadorCarne <= 0)
                {
                    ActivarPestaneo();
                    gm.latidos.GetComponent<AudioSource>().volume = 0;

                }

                if (!esSaludable && gm.contadorCarne <= 1)
                {
                    AumentarVolumenLatidos();
                
                }

                if (!esSaludable && gm.contadorCarne <= 0)
                {
                    Arritmias();
                }

            }


            if (nombreDeProducto == "golosinas")
            {
                gm.contadorGolosinas--;

                if (!esSaludable) {
                    Retorcijon();
                }
            }


            if (nombreDeProducto == "galletitas")
            {
                gm.contadorGalletitas--;
                ActivarEfectos(RalentizarCarrito, AcelerarCarrito);
            }

            if (nombreDeProducto == "cereales")
            {
                gm.contadorCereales--;
                ActivarEfectos(RalentizarCarrito, AcelerarCarrito);
            }




            if (gm.contadorGalletitas != 0 && gm.contadorCereales != 0)
            {
                gm.jugador.GetComponent<PlayerController>().desactivarEfectos = false;

            }

            seEjecutaUnaVez = true;
            //ActivarCaminante(gm.contadorFrutas);
            // ActivarCaminante(gm.contadorGalletitas);
            //// ActivarCaminante(gm.contadorCereales);
            // ActivarCaminante(gm.contadorCarne);
            // ActivarCaminante(gm.contadorGolosinas);

        }
    }






    //--------------- CODIGO DE ActivarCaminante------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        HuboColision();
    }


    //void ActivarCaminante(float contador)
    //{

    //    if (contador == 0)
    //    {
    //        caminante.seMueve = true;
    //        gm.jugador.GetComponent<PlayerController>().desactivarEfectos = true;
    //    }
    //}


    void HuboColision()
    {
        choca = true;
    }

    void ActivarEfectos(Action accionMala, Action accionBuena)
    {
        if (gm.jugador.GetComponent<PlayerController>().desactivarEfectos == false)
        {
            if (!esSaludable)
            {
                accionMala();
            }
            else if (esSaludable)
            {
                accionBuena();
            }
        }
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
        gm.jugador.GetComponent<PlayerController>().speed -= 2;

    }

    void AcelerarCarrito()
    {
        gm.jugador.GetComponent<PlayerController>().speed += 2;
    }


    void Retorcijon()
    {
        gm.retorcijon.GetComponent<AudioSource>().Play();
    }

    void ActivarPestaneo()
    {
        estaBlinkeando = true;
        direccion = 1;
        Tareas.Nueva(1.0f, () => direccion = -1);
        Tareas.Nueva(2.0f, () => estaBlinkeando = false);

        

    }

    void CarritoFuerteClose()// cerrar ojos
    {

        parpados.transform.GetChild(0).transform.localPosition = new Vector3(0, posYparpadoUp, 0);
        parpados.transform.GetChild(1).transform.localPosition = new Vector3(0, posYparpadoInf, 0);

        
    }

    void cambiarCarrito()
    {
        //GameManager.jugador.GetComponent<PlayerController>().carritoBase;
        GameManager.instance.jugador.GetComponent<MeshRenderer>().enabled = true;
    }

    void Arritmias()
    {
        gm.arritmia.GetComponent<AudioSource>().volume = 1;

    }

    void AumentarVolumenLatidos()
    {
        gm.latidos.GetComponent<AudioSource>().volume = 1;
    }

   

}

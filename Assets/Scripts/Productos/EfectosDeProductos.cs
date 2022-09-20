using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EfectosDeProductos : MonoBehaviour
{
    public bool esSaludable;
    GameManager gm;


    //bool veMal = true;


    bool seEjecutaUnaVez;

    //Para el caminante
    public Caminante caminante;
    public string nombreDeProducto;
    bool choca;
    bool desactivarEfecto;


    void Start()
    {
        gm = GameManager.instance;
        choca = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("contGalles" + gm.contadorGalletitas);
        Debug.Log("contFruta" + gm.contadorFrutas);
        Debug.Log("contCarne" + gm.contadorCarne);

        // Debug.Log("contCereales" + gm.contadorCereales);


        if (!seEjecutaUnaVez)
        {

            if (choca)
            {
                if (nombreDeProducto == "bebida")
                {
                    gm.contadorFrutas--;
                }

                if (nombreDeProducto == "carne")
                {
                    gm.contadorCarne--;

                }


                if (nombreDeProducto == "golosinas")
                {
                    gm.contadorGolosinas--;
                }


                if (nombreDeProducto == "galletitas")
                {
                    gm.contadorGalletitas--;

                    ActivarEfectos(RalentizarCarrito, AcelerarCarrito);
                }

                //if (nombreDeProducto == "cereales")
                //{
                //    gm.contadorCereales--;
                //    ActivarEfectos(RalentizarCarrito, AcelerarCarrito);
                //}

                //if (gm.contadorCereales != 0 && gm.contadorCereales != 0) {
                //    gm.jugador.GetComponent<PlayerController>().desactivarEfectos = false;
                //}

                seEjecutaUnaVez = true;
                ActivarCaminante(gm.contadorFrutas);
                ActivarCaminante(gm.contadorGalletitas);
                // ActivarCaminante(gm.contadorCereales);
                ActivarCaminante(gm.contadorCarne);
                ActivarCaminante(gm.contadorGolosinas);
            }
        }


        //if (nombreDeProducto == "vision")
        //{
        //    veMal = false;

        //}

    }

    //--------------- CODIGO DE ActivarCaminante------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        HuboColision();
    }


    void ActivarCaminante(float contador)
    {

        if (contador == 0)
        {
            caminante.seMueve = true;
            gm.jugador.GetComponent<PlayerController>().desactivarEfectos = true;

        }
    }


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

    }

    void CarritoResbaloso()
    {

    }

    void CarritoFuerte()
    {
    }

    void Arritmias()
    {
    }

}

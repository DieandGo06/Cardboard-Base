using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public Caminante caminanteFrutas;
    //public Caminante caminanteGalletitas;
    //public Caminante caminanteCereales;
    public GameObject tachaduraGalletitas;
    public GameObject tachaduraCereal;
    public GameObject tachaduraFruta;
    public GameObject tachaduraVerdura;
    public GameObject tachaduraCarne;
    public GameObject tachaduraGolosina;

    public GameObject bebida;
    public GameObject latidos;
    public GameObject arritmia;
    public GameObject jugador;


    //public PlayerInfo playerInfo;

    //public RawImage corteDeLuz;
    //public GameObject globalVolume;

    private int limiteDeFps = 30;
    public float contadorFrutas;
    public float contadorVerduras;
    public float contadorGalletitas;
    public float contadorCereales;
    public float contadorCarne;
    public float contadorGolosinas;

    //------------------------------------------------SONIDO
    //public AudioSource audio;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = limiteDeFps;
        contadorFrutas = 1;
        contadorVerduras = 2;
        contadorGalletitas = 2;
        contadorCereales = 2;
        contadorCarne = 2;
        contadorGolosinas = 1;

    }

    private void Update()
    {

        if (jugador != null)
        {


            if (contadorGalletitas == 0)
            {

              
                ActivarCaminante(tachaduraGalletitas);

            }

            if (contadorCereales == 0)
            {
              
                ActivarCaminante(tachaduraCereal);
            }

            if (contadorFrutas == 0)
            {             
                ActivarCaminante(tachaduraFruta);
            }

            if (contadorVerduras == 0)
            {
                ActivarCaminante(tachaduraVerdura);
            }

            if (contadorCarne == 0)
            {

   
                ActivarCaminante(tachaduraCarne);

            }

            if (contadorGolosinas == 0)
            {
                ActivarCaminante(tachaduraGolosina);
               

            }
        }


    }

    //void ActivarCaminante(Caminante caminante)
    //{
    //    caminante.seMueve = true;
    //    jugador.GetComponent<PlayerController>().desactivarEfectos = true;
    //}

    void ActivarCaminante(GameObject tachadura)
    {
        tachadura.SetActive(true);
        jugador.GetComponent<PlayerController>().desactivarEfectos = true;
    }




}

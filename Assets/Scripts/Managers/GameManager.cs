using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Caminante caminanteFrutas;
    public Caminante caminanteGalletitas;
    public Caminante caminanteCereales;
    //public GameObject suelo;
    //public GameObject paredes;
    public GameObject bebida;
    public GameObject latidos;
    public GameObject arritmia;
    public GameObject jugador;
    //public PlayerInfo playerInfo;

    //public RawImage corteDeLuz;
    //public GameObject globalVolume;

    private int limiteDeFps = 30;
    public float contadorBebida;
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
        contadorBebida = 2;
        contadorGalletitas = 2;
        contadorCereales = 2;
        contadorCarne = 2;
        contadorGolosinas = 1;

    }

    private void Update()
    {

        if (jugador != null) {
            if (contadorBebida == 0)
            {
                ActivarCaminante(caminanteFrutas);
                
            }

            if (contadorGalletitas == 0)
            {
                ActivarCaminante(caminanteGalletitas);
            
            }

            if (contadorCereales == 0)
            {
                ActivarCaminante(caminanteCereales);
            }
        }
    

    }

    void ActivarCaminante(Caminante caminante) {
        caminante.seMueve = true;
        jugador.GetComponent<PlayerController>().desactivarEfectos = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //public GameObject suelo;
    //public GameObject paredes;

    //public GameObject jugador;
    //public PlayerInfo playerInfo;

    //public RawImage corteDeLuz;
    //public GameObject globalVolume;

    private int limiteDeFps = 30;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
     Application.targetFrameRate = limiteDeFps;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static string nivel;
    public static float segundos;
    public static int minutos;
    public Text tiempoText;

	// Use this for initialization
	void Start () {
        if(!(Juego.modoAventura && nivel != "Nivel01")) {
            segundos = 0.0f;
            minutos = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
        segundos += Time.deltaTime;

        /* if (segundos >= 30 && segundos <= 31)
            Probabilidades.prob_bueno += 5; */

        if(segundos >= 60) {
            minutos++;

            Probabilidades.prob_bueno += 10;

            segundos = 0.0f;
        }

        string tiempo = "Tiempo:  ";
        if (minutos < 10)
            tiempo += "0";
        tiempo += minutos + ":";
        if (segundos < 10)
            tiempo += "0";
        tiempo += (int) segundos;
        tiempoText.text = tiempo;
	}
}

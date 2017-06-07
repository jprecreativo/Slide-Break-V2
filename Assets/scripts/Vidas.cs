using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    public static int vidas;
    public Image corazon1, corazon2, corazon3, corazon4, corazon5;
    public Pelota pelota;
    public Barra barra;
    public Timer timer;
    public GameObject gameOverText;
    public GameObject botonVolver;
    public AudioSource asFondo;
    public AudioSource asError;
    public AudioClip gameOver;

	// Use this for initialization
	void Start () {
        if (vidas < 5)
            corazon5.gameObject.SetActive(false);
        if (vidas < 4)
            corazon4.gameObject.SetActive(false);
        if (vidas < 3)
            corazon3.gameObject.SetActive(false);
        if (vidas < 2)
            corazon2.gameObject.SetActive(false);

        asFondo.volume = Opciones.volumen;
        asError.volume = Opciones.volumen;
	}

    public void perdervida(bool reset) { // si reset es true, se reseteará la pelota y la barra (que será cuando la pelota toque el suelo)
        vidas--;

        if (vidas == 4)
            corazon5.gameObject.SetActive(false);
        else if (vidas == 3)
            corazon4.gameObject.SetActive(false);
        else if (vidas == 2)
            corazon3.gameObject.SetActive(false);

        else if (vidas == 1)
        {
            // Como me queda una vida, hago menos probable que salga el 'perder vida' y más probable el 'ganar vida':

            Probabilidades.cambiarProbsObjeto(false, Probabilidades.RESTAVIDA, -3);
            Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUMAVIDA, +4);

            corazon2.gameObject.SetActive(false);
        }
            

        else if (vidas == 0)
            corazon1.gameObject.SetActive(false);

        if (vidas == 0) {
            comprobarFlags();

            gameOverText.SetActive(true);
            botonVolver.SetActive(true);

            Pelota.enJuego = false;
            pelota.detenerMovimiento();
            barra.enabled = false;
            timer.enabled = false;

            asFondo.clip = gameOver;
            asFondo.loop = false;
            asFondo.Play();
        }
        else {
            asError.Play();

            if (reset) {
                barra.reset();
                pelota.reset();
                comprobarFlags();
            }
        }
    }

    private void comprobarFlags() {
        if (Disparos.disparar)
            Disparos.disparar = false;
        if (Barra.dislexia)
            Barra.dislexia = false;
        if (Barra.tortuga)
            Barra.tortuga = false;
        if (Pelota.iman)
            Pelota.iman = false;
    }

    public void ganarVida() {
        if (++vidas > 5)
            vidas = 5;

        if (vidas == 2)
            corazon2.gameObject.SetActive(true);
        else if (vidas == 3)
            corazon3.gameObject.SetActive(true);
        else if (vidas == 4)
            corazon4.gameObject.SetActive(true);

        else if (vidas == 5)
        {
            // Como tengo 5 vidas, hago menos probable que salga 'ganar vida' y más probable 'perder vida':

            Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUMAVIDA, -4);
            Probabilidades.cambiarProbsObjeto(false, Probabilidades.RESTAVIDA, +3);

            corazon5.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

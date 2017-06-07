using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarOpciones : MonoBehaviour {

    public AudioSource musicaFondo;

	// Use this for initialization
	void Start () {
        musicaFondo.volume = Opciones.volumen;
	}

    private void Awake() {
        if (PlayerPrefs.GetInt("opcionesGuardadas") == 0) { // aun no se guardó opciones, por tanto, se cargan unas por defecto
            Opciones.dificultad = 1; // nivel normal
            Opciones.volumen= 40; // volumen inicial (40%)
        }
        else { // hay datos guardados, se cargan
            // Debug.Log("Dificultad que cargo: " + PlayerPrefs.GetFloat("dificultad"));

            Opciones.dificultad = PlayerPrefs.GetFloat("dificultad");

            // Debug.Log("Volumen que cargo :" + PlayerPrefs.GetFloat("volumen"));

            Opciones.volumen = PlayerPrefs.GetFloat("volumen");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

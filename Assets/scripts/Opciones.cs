using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour {

    public static float dificultad, volumen;
    public Slider sliderDificultad, sliderVolumen;
    public Text valorDificultad, valorVolumen;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
        sliderDificultad.onValueChanged.AddListener(delegate { cambioDificultad(); });
        sliderVolumen.onValueChanged.AddListener(delegate { cambioVolumen(); });

        sliderDificultad.value = dificultad;
        sliderVolumen.value = PlayerPrefs.GetFloat("volumen") * 100;

        // Debug.Log("El volumen en el start de Opciones vale: " + PlayerPrefs.GetFloat("volumen"));

        audioSource.volume = PlayerPrefs.GetFloat("volumen");
	}

    private void cambioVolumen() {
        valorVolumen.text = sliderVolumen.value + "%";
        audioSource.volume = sliderVolumen.value / 100;
        volumen = sliderVolumen.value;
    }

    private void cambioDificultad() {
        if (sliderDificultad.value == 0) {
            valorDificultad.text = "Fácil";
        }
        else if (sliderDificultad.value == 1) {
            valorDificultad.text = "Normal";
        }
        else {
            valorDificultad.text = "Difícil";
        }
        dificultad = sliderDificultad.value;
    }
	
	// Update is called once per frame
	void Update () {

        

    }

    private void OnDestroy() {
        // guardo las opciones
        PlayerPrefs.SetInt("opcionesGuardadas", 1);
        PlayerPrefs.SetFloat("dificultad", dificultad);

        // Debug.Log("Dificultad antes de destruirme: " + dificultad);
        // Debug.Log("Volumen antes de destruirme: " + volumen / 100);

        PlayerPrefs.SetFloat("volumen", volumen / 100);
    }
}

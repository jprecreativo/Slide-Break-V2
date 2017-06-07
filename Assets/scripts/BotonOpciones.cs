using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonOpciones : MonoBehaviour {

    public Button botonOpciones;

	// Use this for initialization
	void Start () {
        botonOpciones.onClick.AddListener(abrirOpciones);
	}

    private void abrirOpciones() {
        SceneManager.LoadScene("Opciones");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

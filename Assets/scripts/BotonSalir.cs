using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSalir : MonoBehaviour {

    public Button botonSalir;

	// Use this for initialization
	void Start () {
        botonSalir.onClick.AddListener(salir);
	}

    private void salir() {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

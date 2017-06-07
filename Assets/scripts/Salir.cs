using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour {

    public bool salir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel")) { // Cancel: tecla escape 
            if (salir) { // estoy en el menú
                Application.Quit();
            }
            else { // estoy en un nivel o en opciones
                SceneManager.LoadScene("Menu");
            }
        }
	}
}

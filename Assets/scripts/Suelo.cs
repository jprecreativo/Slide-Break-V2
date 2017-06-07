using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suelo : MonoBehaviour {

    public Vidas vidas;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Pelota") {
            vidas.perdervida(true); // true para que se resetee la barra y la pelota
        }
        else { // si no es la pelota, es un objeto
            Destroy(collider.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

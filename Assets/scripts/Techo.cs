using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Techo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Proyectil") {
            Destroy(collider.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour {

    public GameObject barra, bolaFuego;
    public float min, max; // min y max de tiempo entre 2 bolas lanzadas
    public float velocidad;

    private GameObject temporal;
    private float segundos, siguienteBola;

	// Use this for initialization
	void Start () {
        segundos = 0;
        siguienteBola = Random.Range(min, max);
	}
	
	// Update is called once per frame
	void Update () {
        segundos += Time.deltaTime;

        if (segundos > siguienteBola) { 
            float dirX = (barra.transform.position.x - transform.position.x) * 23;
            Vector3 direccion = new Vector3(dirX, velocidad, 0);
            temporal = Instantiate(bolaFuego, transform.position, Quaternion.identity);
            temporal.name = "BolaFuego";
            temporal.GetComponent<Rigidbody>().AddForce(direccion);

            siguienteBola += Random.Range(min, max);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour {

    public GameObject proyectil;
    public static bool disparar; // cuando sea true la barra comenzará a disparar proyectiles
    public static bool primer_disparo; // si es el primer disparo, para inicializar el temporizador
    public GameObject Barra;

    private float segundos; // contador de los segundos que lleva disparando
    private float siguiente_disparo; // indicará cuando se debe producir el siguiente disparo
    private int disparos; // disparos totales al coger item proyectiles
    private float intervalo; // intervalo de tiempo entre un disparo y otro (segundos)
    private GameObject temporal; // objeto temporal para asignarle nombre a los objetos instanciados en tiempo de ejecución

	// Use this for initialization
	void Start () {
        disparar = false;
        primer_disparo = true;
        segundos = 0;
        siguiente_disparo = 0;

        if (Opciones.dificultad == 0)   // Dificultad fácil.
            disparos = 10;

        else if (Opciones.dificultad == 1)   // Dificultad normal.
            disparos = 7;

        else
            disparos = 3;

        intervalo = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        // disparando proyectiles
        if (disparar) {

            if (primer_disparo) {
                primer_disparo = false;
                segundos = 0;
                siguiente_disparo = 0;

                if (Opciones.dificultad == 0)   // Dificultad fácil.
                    disparos = 10;

                else if (Opciones.dificultad == 1)   // Dificultad normal.
                    disparos = 7;

                else
                    disparos = 3;
            }

            segundos += Time.deltaTime;

            if (segundos > siguiente_disparo && disparos > 0) {
                // se realiza disparo
                Vector3 pos_proyectil_izq = new Vector3(Barra.transform.position.x - 1.8f, -7, 0);
                Vector3 pos_proyectil_der = new Vector3(Barra.transform.position.x + 1.8f, -7, 0);
                temporal = Instantiate(proyectil, pos_proyectil_izq, Quaternion.identity);
                temporal.name = "Proyectil";
                temporal.GetComponent<Rigidbody>().AddForce(new Vector3(0, 600, 0));
                temporal = Instantiate(proyectil, pos_proyectil_der, Quaternion.identity);
                temporal.name = "Proyectil";
                temporal.GetComponent<Rigidbody>().AddForce(new Vector3(0, 600, 0));

                siguiente_disparo += intervalo;
                disparos--;
            }

            if (disparos == 0) {
                disparar = false;
            }
        }
	}
}

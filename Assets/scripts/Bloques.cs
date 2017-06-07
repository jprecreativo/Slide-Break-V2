using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloques : MonoBehaviour {

    public GameObject efectoParticulas, efectoParticulas2, efectoParticulas3, efectoParticulas4;
    public GameObject sumaVida, restaVida, achicar, agrandar, proyectiles, dislexia, sueloExtra, tortuga, iman;
    public Puntos puntos;
    public Material material3, material2, material1;

    private GameObject temporal; // objeto temporal para asignarle nombre a los objetos instanciados en tiempo de ejecución

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision) { 
        if(collision.gameObject.name == "Pelota")
            golpeBloque();
    }

    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.name == "Proyectil"){
            golpeBloque();
            Destroy(collider.gameObject);
        }
    }

    private void golpeBloque() {

        puntos.sumarPunto();

        // se calcula probabilidad de soltar objeto
        if ((int)Random.Range(1, 101) <= Probabilidades.prob_objeto) {  // suelta objeto

            // se calcula si es objeto bueno o malo
            if ((int)Random.Range(1, 101) <= Probabilidades.prob_bueno) { // objeto bueno

                int rand = (int)Random.Range(1, 101);

                if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.SUMAVIDA]) {
                    temporal = Instantiate(sumaVida, transform.position, Quaternion.identity);
                    temporal.name = "SumaVida";

                    Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUMAVIDA, -8);
                }
                else if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.AGRANDAR]) {
                    temporal = Instantiate(agrandar, transform.position, Quaternion.identity);
                    temporal.name = "Agrandar";

                    Probabilidades.cambiarProbsObjeto(true, Probabilidades.AGRANDAR, -12);
                }
                else if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.PROYECTILES]) {
                    temporal = Instantiate(proyectiles, transform.position, Quaternion.identity);
                    temporal.name = "Proyectiles";

                    Probabilidades.cambiarProbsObjeto(true, Probabilidades.PROYECTILES, -4);
                }
                else if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.SUELOEXTRA]) {
                    temporal = Instantiate(sueloExtra, transform.position, Quaternion.identity);
                    temporal.name = "SueloExtra";

                    Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUELOEXTRA, -8);
                }
                else { // IMAN
                    temporal = Instantiate(iman, transform.position, Quaternion.identity);
                    temporal.name = "Iman";

                    Probabilidades.cambiarProbsObjeto(true, Probabilidades.IMAN, -8);
                }
            }
            else { // objeto malo

                int rand = (int)Random.Range(1, 101);

                if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.RESTAVIDA]) {
                    temporal = Instantiate(restaVida, transform.position, Quaternion.identity);
                    temporal.name = "RestaVida";

                    Probabilidades.cambiarProbsObjeto(false, Probabilidades.RESTAVIDA, -9);
                }
                else if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.ACHICAR]) {
                    temporal = Instantiate(achicar, transform.position, Quaternion.identity);
                    temporal.name = "Achicar";

                    Probabilidades.cambiarProbsObjeto(false, Probabilidades.ACHICAR, -12);
                }
                else if (rand <= Probabilidades.delimitaciones_buenos[Probabilidades.DISLEXIA]) {
                    temporal = Instantiate(dislexia, transform.position, Quaternion.identity);
                    temporal.name = "Dislexia";

                    Probabilidades.cambiarProbsObjeto(false, Probabilidades.DISLEXIA, -3);
                }
                else { // TORTUGA
                    temporal = Instantiate(tortuga, transform.position, Quaternion.identity);
                    temporal.name = "Tortuga";

                    Probabilidades.cambiarProbsObjeto(false, Probabilidades.TORTUGA, -9);
                }
            }
        }

        // se cambia el material del bloque tocado por el material del nivel inferior, o se destruye si es bloque nivel 1
        if (gameObject.tag.Equals("Bloque_nivel_4")) {
            gameObject.GetComponent<Renderer>().material = material3;
            gameObject.tag = "Bloque_nivel_3";
            Instantiate(efectoParticulas4, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity); // crea un efecto de particulas en la posicion del bloque destruido
        }
        else if (gameObject.tag.Equals("Bloque_nivel_3")) {
            gameObject.GetComponent<Renderer>().material = material2;
            gameObject.tag = "Bloque_nivel_2";
            Instantiate(efectoParticulas3, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity); // crea un efecto de particulas en la posicion del bloque destruido
        }
        else if (gameObject.tag.Equals("Bloque_nivel_2")) {
            gameObject.GetComponent<Renderer>().material = material1;
            gameObject.tag = "Bloque_nivel_1";
            Instantiate(efectoParticulas2, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity); // crea un efecto de particulas en la posicion del bloque destruido
        }
        else {
            Destroy(gameObject); // destruye el bloque
            Instantiate(efectoParticulas, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),
                Quaternion.identity); // crea un efecto de particulas en la posicion del bloque destruido
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

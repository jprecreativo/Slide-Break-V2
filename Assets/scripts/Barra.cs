using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barra : MonoBehaviour {

    public float velocidad; // velocidad de desplazamiento de la barra
    public Vidas vidas;
    public static bool dislexia; // si es true, los controles de la barra se intercambian durante un tiempo
    public static bool tortuga; // si es true, la barra se moverá más lenta durante un tiempo
    public GameObject sueloExtra, pelota;

    private Vector3 posInicial; // posicion inicial de la barra
    private float limDer; // limite derecho de desplazamiento de la barra
    private float limIzq; // limite izquierdo de desplazamiento de la barra
    private float segundosDislexia; // lleva la cuenta del tiempo con dislexia
    private float segundosTortuga; // lleva la cuenta del tiempo con la barra lenta

	// Use this for initialization
	void Start () {
        velocidad = 20;
        dislexia = false;
        tortuga = false;
        limDer = 7.5f;
        limIzq = -7.5f;
        segundosDislexia = 0;
        segundosTortuga = 0;
        posInicial = transform.position;
	}

    // cuando pierde una vida, resetea los valores
    public void reset() {
        transform.position = posInicial;
    }

    private void OnTriggerEnter(Collider collider) {
        if (Pelota.enJuego) {
            if (collider.gameObject.name == "RestaVida") {
                // pierde una vida
                vidas.perdervida(false); // false porque no queremos que se resetee la barra y la pelota

                Probabilidades.cambiarProbsObjeto(false, Probabilidades.RESTAVIDA, -9);
            }
            else if (collider.gameObject.name == "SumaVida") {
                // suma una vida hasta un máximo de 5
                vidas.ganarVida();

                Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUMAVIDA, -8);
            }
            else if (collider.gameObject.name == "Achicar") {

                Probabilidades.cambiarProbsObjeto(false, Probabilidades.ACHICAR, -12);

                bool achicarConIman = false;
                if (Pelota.iman) {
                    if (pelota.transform.parent != null) {
                        pelota.transform.SetParent(null);
                        achicarConIman = true;
                    }
                }
                // reduce la longitud de la barra
                if ((transform.localScale.x - 1) >= 2) {
                    transform.localScale -= new Vector3(1, 0, 0);
                    limIzq -= 0.5f;
                    limDer += 0.5f;
                }

                if (achicarConIman)
                    pelota.transform.SetParent(transform);
            }
            else if (collider.gameObject.name == "Agrandar") {

                Probabilidades.cambiarProbsObjeto(true, Probabilidades.AGRANDAR, -12);

                bool agrandarConIman = false;
                if (Pelota.iman)
                {
                    if (pelota.transform.parent != null)
                    {
                        pelota.transform.SetParent(null);
                        agrandarConIman = true;
                    }
                }
                // aumenta la longitud de la barra
                if ((transform.localScale.x + 2) <= 8) {
                    transform.localScale += new Vector3(2, 0, 0);
                    limIzq += 1;
                    limDer -= 1;
                }

                if (agrandarConIman)
                    pelota.transform.SetParent(transform);
            }
            else if (collider.gameObject.name == "Proyectiles") { 

                // activamos el flag para que comienze la barra a disparar
                Disparos.disparar = true;
                Disparos.primer_disparo = true;

                // Como he cogido el disparo, disminuyo su probabilidad:

                Probabilidades.cambiarProbsObjeto(true, Probabilidades.PROYECTILES, -4);

            }
            else if(collider.gameObject.name == "Dislexia") {
                // activamos el flag para que se intercambien los controles de la barra
                dislexia = true;
                segundosDislexia = 0;

                Probabilidades.cambiarProbsObjeto(false, Probabilidades.DISLEXIA, -3);
            }
            else if (collider.gameObject.name == "BolaFuego") {
                // pierde una vida
                vidas.perdervida(true); // true porque queremos que se resetee la barra y la pelota
            }
            else if (collider.gameObject.name == "SueloExtra") {
                // activa el suelo extra que te protege 1 vez
                sueloExtra.SetActive(true);

                Probabilidades.cambiarProbsObjeto(true, Probabilidades.SUELOEXTRA, -8);
            }
            else if (collider.gameObject.name == "Tortuga") {
                // activamos el flag para que la barra vaya lenta durante unos segundos
                tortuga = true;
                segundosTortuga = 0;

                Probabilidades.cambiarProbsObjeto(false, Probabilidades.TORTUGA, -9);
            }
            else if (collider.gameObject.name == "Iman") {
                // activamos el flag para que la barra tenga imán con la pelota
                Pelota.iman = true;
                Pelota.segundosIman = 0;

                Probabilidades.cambiarProbsObjeto(true, Probabilidades.IMAN, -8);
            }
        }

        // destruye el objeto
        Destroy(collider.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        // desplazamiento lateral de la barra
        float tecladoHorizontal = Input.GetAxisRaw("Horizontal"); // -1 izquierda, 1 derecha

        // dislexia
        if (dislexia && segundosDislexia < 5) {
            segundosDislexia += Time.deltaTime;
            tecladoHorizontal *= -1; // cambiamos el sentido del movimiento
        }
        if (segundosDislexia > 5) {
            dislexia = false;
        }

        // se calcula siguiente posición de la barra
        float posX = transform.position.x + (tecladoHorizontal * velocidad * Time.deltaTime);

        // tortuga
        if (tortuga && segundosTortuga < 5) {
            segundosTortuga += Time.deltaTime;
            posX = transform.position.x + (tecladoHorizontal * (velocidad/3) * Time.deltaTime);
        }
        if (segundosTortuga > 5) {
            tortuga = false;
        }
        
        transform.position = new Vector3(Mathf.Clamp(posX, limIzq, limDer), transform.position.y, transform.position.z); // el metodo Clamp hace que posX nunca se salga del intervalo (limIzq,limDer)
	}
}

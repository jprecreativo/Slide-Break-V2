using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pelota : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 posInicial;
    private Transform barra;

    public static bool enJuego;
    public static int toques;
    public float velocidadInicial;
    public Text toquesText;
    public AudioSource rebote;
    public AudioSource punto;
    public GameObject sueloExtra;
    public static bool iman; // si es true, al colisionar la pelota con la barra, quedará pegada hasta pulsar tecla Fire1
    public static float segundosIman;

    // en awake, obtendremos las referencias a los componentes
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        barra = transform.parent;
    }

	// Use this for initialization
	void Start () {
        enJuego = false;
        velocidadInicial = 600;
        iman = false;
        segundosIman = 0;
        posInicial = transform.position;
        toquesText.text = "Toques:  " + toques;
        rebote.volume = Opciones.volumen;
        punto.volume = Opciones.volumen;
	}

    // cuando pierde una vida, resetea los valores
    public void reset() {
        transform.position = posInicial;
        transform.SetParent(barra); // volvemos a poner la pelota como hijo de la barra
        enJuego = false;
        detenerMovimiento();
    }

    public void detenerMovimiento() {
        rb.isKinematic = true; // ya no se ve afectado por las fisicas
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision) {
        // conteo de toques con la barra, efecto imán e impulso pelota
        if (collision.gameObject.name == "Barra") {
            toques++;
            toquesText.text = "Toques:  " + toques;
            
            if (iman) {
                transform.SetParent(barra);
                transform.position = new Vector3(transform.position.x, -6.9f, transform.position.z);
                detenerMovimiento();
            }
            else { 
                float x, y;
                float direccion = Input.GetAxisRaw("Horizontal");

                // si la barra está quieta, no aplicamos fuerza, si se está moviendo, aplicamos fuerza en esa dirección
                if (direccion != 0) { // barra moviéndose 
                    // componente x
                    // si viene con poca fuerza, se le da hasta llegar a velocidadInicial, si viene con más, se deja tal cual
                    // si la barra se mueve al contrario que viene la pelota, se aplica la fuerza necesario para cambiarle sentido
                    if (rb.velocity.x >= 0) 
                        if (rb.velocity.x < 12)
                            if (direccion == 1)
                                x = (12 - rb.velocity.x) * 50;
                            else
                                x = (rb.velocity.x * 50 + 600) * (-1);
                        else // >= 12
                            if (direccion == 1)
                                x = 0;
                            else
                                x = (rb.velocity.x * 50 * 2) * (-1);
                    else // < 0
                        if (rb.velocity.x > -12)
                            if (direccion == 1)
                                x = (rb.velocity.x * 50 + 600) * (-1);
                            else
                                x = (-12 - rb.velocity.x) * 50;
                        else // <= -12
                            if (direccion == 1)
                                x = (rb.velocity.x * 50 * 2) * (-1);
                            else
                                x = 0;

                    // componente y
                    if (rb.velocity.y < 12)
                        y = (12 - rb.velocity.y) * 50; // le damos fuerza hasta llegar a velocidadIninical
                        // el 50 viene de aquí: dar una fuerza de 600 implica una velocidad de 12 (50 veces menos)
                    else
                        y = 0; // si viene con alta velocidad, no le doy más

                    rb.AddForce(new Vector3(x, y, 0));
                }

                /*if (transform.position.x > collision.gameObject.transform.position.x + 1) { // si pelota golpea en extremo derecho de barra
                    rb.AddForce(new Vector3(velocidadInicial / 2, 0, 0)); // impulso hacia la derecha
                }
                else if (transform.position.x < collision.gameObject.transform.position.x - 1) { // si golpea en extremo izquierdo de barra
                    rb.AddForce(new Vector3(-velocidadInicial / 2, 0, 0)); // impulso hacia la izquierda
                }*/
            }
        }
        // impulso extra al colisionar con las columnas y techo
        else if (collision.gameObject.name == "ColumnaIzquierda") {
            //rb.AddForce(new Vector3(1, 2, 0));
            if (rb.velocity.y < 0)
                rb.AddForce(new Vector3(1,-2,0));
            else
                rb.AddForce(new Vector3(1, 2, 0));
        }
        else if (collision.gameObject.name == "ColumnaDerecha") {
            //rb.AddForce(new Vector3(-1, 2, 0));
            if (rb.velocity.y < 0)
                rb.AddForce(new Vector3(-1, -2, 0));
            else
                rb.AddForce(new Vector3(-1, 2, 0));
        }
        else if (collision.gameObject.name == "Techo") {
            rb.AddForce(new Vector3(1, 0, 0));
        }
        else if (collision.gameObject.name == "SueloExtra") {
            toques++;
            toquesText.text = "Toques:  " + toques;
            //rb.AddForce(new Vector3(1, 1, 0));
            sueloExtra.SetActive(false);
        }

        // reproduccion de sonidos
        if (collision.gameObject.CompareTag("Rebote")) // estructura o barra
            rebote.Play();
        else
            punto.Play(); // bloques
    }
	
	// Update is called once per frame
	void Update () {

        // comienzo de la partida
        if((!enJuego || iman) && Input.GetButtonDown("Fire1")) { // Fire1: space or enter
            if (transform.parent != null) {
                impulsoInicial();
            }
        }

        // iman
        if (iman) {
            segundosIman += Time.deltaTime;
            if (segundosIman > 10) {
                iman = false;
                if (transform.parent != null) {
                    impulsoInicial();
                }
            }
        }
	}

    private void impulsoInicial() {
        enJuego = true;
        transform.SetParent(null); // hacemos que la pelota deje de ser hija de la barra
        rb.isKinematic = false; // el RigidBody ya es afectado por las físicas

        // damos fuerza a la pelota, según se esté moviendo o no la barra
        rb.AddForce(new Vector3(velocidadInicial * Input.GetAxisRaw("Horizontal"), velocidadInicial, 0));
    }
}

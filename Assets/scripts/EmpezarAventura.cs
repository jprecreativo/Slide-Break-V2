using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmpezarAventura : MonoBehaviour
{
    public Button botonAventura;
    
	private void Start ()
    {
        botonAventura.onClick.AddListener(empezarAventura);
	}

    private void empezarAventura()
    {
        Probabilidades.inicializarProbabilidades();

        Juego.modoAventura = true;
        Pelota.toques = 0;
        Timer.minutos = 0;
        Timer.segundos = 0.0f;
        Puntos.puntos = 0;
        Timer.nivel = "Nivel01";
        SceneManager.LoadScene("Nivel01");
    }
}

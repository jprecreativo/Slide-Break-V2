using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puntuacion : MonoBehaviour
{
    public const int PUNTOS_BASE = 1000;
    public static int penalizacionSegundos;
    public static int penalizacionToques;
    public static int puntosTotales;
    public Button puntuacion;

	private void Start ()
    {
        puntuacion.onClick.AddListener(calcularPuntos);	
	}

    private static void calcularPuntos()
    {
        penalizacionSegundos = (int) (((Timer.minutos * 60) + Timer.segundos) * 0.4);
        penalizacionToques = (int) (Pelota.toques * 0.6);

        int dificultad = (int) PlayerPrefs.GetFloat("dificultad");

        puntosTotales = (PUNTOS_BASE - penalizacionSegundos - penalizacionToques + Vidas.vidas) * (dificultad + 1);

        Juego.verRanking = false;

        SceneManager.LoadScene("Puntuaciones");
    }
}

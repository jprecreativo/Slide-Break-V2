using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EmpezarEstandar : MonoBehaviour
{
    public Button btEstandar;
	
	private void Start ()
    {
        btEstandar.onClick.AddListener(empezarEstandar);
	}
	
	private void empezarEstandar()
    {
        Probabilidades.inicializarProbabilidades();

        Juego.modoAventura = false;
        Pelota.toques = 0;
        Timer.minutos = 0;
        Timer.segundos = 0.0f;
        Puntos.puntos = 0;
        SceneManager.LoadScene("ScreenStandard");
    }
}

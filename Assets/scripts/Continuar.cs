using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continuar : MonoBehaviour
{
    public string siguienteNivel;
    public Button botonContinuar;

	void Start ()
    {
        botonContinuar.onClick.AddListener(continuar);
	}

    private void continuar()
    {
        if(Juego.modoAventura)
        {
            Puntos.puntos = 0;

            // Sumamos 1 vida al pasar al siguiente nivel hasta un máximo de 5 vidas.

            if (++Vidas.vidas > 5)
                Vidas.vidas = 5;

            // Al pasar de nivel, la probabilidad de que salga un objeto bueno disminuye en 25.

            Probabilidades.prob_bueno -= 25;

            Timer.nivel = siguienteNivel;
            SceneManager.LoadScene(siguienteNivel);
        }

        else
            SceneManager.LoadScene("Menu");
    }
}

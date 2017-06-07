using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VerRanking : MonoBehaviour
{
    public Button btRanking;

	private void Start ()
    {
        btRanking.onClick.AddListener(mostrarRanking);
	}

    private void mostrarRanking()
    {
        Juego.verRanking = true;

        SceneManager.LoadScene("Puntuaciones");
    }
}

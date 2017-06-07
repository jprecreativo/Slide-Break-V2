using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{
    public Button botonVolver;

	private void Start ()
    {
        botonVolver.onClick.AddListener(volver);
	}

    private void volver() {
        SceneManager.LoadScene("Menu");
    }
}

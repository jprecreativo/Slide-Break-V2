using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElegirNivel : MonoBehaviour
{
    public static string nivelStandard;
    public AudioSource volumen;
    public Button nivel1, nivel2, nivel3;
	
	private void Start ()
    {
        volumen.volume = PlayerPrefs.GetFloat("volumen");

        nivel1.onClick.AddListener(delegate { jugarNivel(1); });
        nivel2.onClick.AddListener(delegate { jugarNivel(2); });
        nivel3.onClick.AddListener(delegate { jugarNivel(3); });
    }
	
    private void jugarNivel(int bt)
    {
        nivelStandard = "Nivel0" + bt;

        SceneManager.LoadScene(nivelStandard);
    }
}

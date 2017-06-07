using UnityEngine;

public class Puntos : MonoBehaviour
{
    public static int puntos;
    public int maxPuntos;
    public bool ultimoNivel;
    public GameObject nivelCompletadoText;
    public GameObject juegoTerminadoText;
    public GameObject botonContinuar;
    public GameObject botonPuntuacion;
    public Pelota pelota;
    public Barra barra;
    public Timer timer;
    public AudioSource audioSource;
    public AudioClip completado;

	private void Start ()
    {
        audioSource.volume = Opciones.volumen;
	}

    public void sumarPunto()
    {
        puntos++;

        if (puntos == maxPuntos)
        {
            Pelota.enJuego = false;
            comprobarFlags();
            pelota.detenerMovimiento();
            barra.enabled = false;
            timer.enabled = false;

            audioSource.clip = completado;
            audioSource.loop = false;
            audioSource.Play();

            if (Juego.modoAventura)
                if (ultimoNivel)
                {
                    juegoTerminadoText.SetActive(true);
                    botonPuntuacion.SetActive(true);
                }
                else
                {
                    nivelCompletadoText.SetActive(true);
                    botonContinuar.SetActive(true);
                
                }

            else   // Modo estándar.
            {  
                nivelCompletadoText.SetActive(true);
                botonPuntuacion.SetActive(true);
            }
        }
    }

    private void comprobarFlags()
    {
        if (Disparos.disparar)
            Disparos.disparar = false;

        if (Barra.dislexia)
            Barra.dislexia = false;

        if (Barra.tortuga)
            Barra.tortuga = false;

        if (Pelota.iman)
            Pelota.iman = false;
    }
}

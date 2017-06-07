using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    private string[] rankingAventura, rankingStandard1, rankingStandard2, rankingStandard3;
    private int tamaRanking;

    public InputField nombre;
    public AudioSource volumen;
    public Button btAceptar, btBorrarRanking;
    public Text puntosBase, penalizacionTiempo, penalizacionToques, vidas, puntosFinales;
    public GameObject objetoRanking;
    public Toggle aventura, standard, nivel1, nivel2, nivel3;

    private void Awake()
    {
        tamaRanking = objetoRanking.transform.childCount;

        rankingAventura = new string[tamaRanking];
        rankingStandard1 = new string[tamaRanking];
        rankingStandard2 = new string[tamaRanking];
        rankingStandard3 = new string[tamaRanking];
    }

    private void Start ()
    {
        this.inicializarResumen();
        this.cargarRecords();

        volumen.volume = PlayerPrefs.GetFloat("volumen");
        btBorrarRanking.onClick.AddListener(limpiarRanking);

        if(!Juego.verRanking)
        {
            nombre.enabled = true;
            btAceptar.onClick.AddListener(guardarRecord);
        }

        else
            nombre.enabled = false;

        aventura.onValueChanged.AddListener((flag) =>
        {
            mostrarRecords();
        });

        standard.onValueChanged.AddListener((flag) =>
        {
            mostrarRecords();
        });

        nivel1.onValueChanged.AddListener((flag) =>
        {
            mostrarRecords();
        });

        nivel2.onValueChanged.AddListener((flag) =>
        {
            mostrarRecords();
        });

        nivel3.onValueChanged.AddListener((flag) =>
        {
            mostrarRecords();
        });
    }

    private void cargarRecords()
    {
        for(int i = 0; i < tamaRanking; i++)
        {
            rankingAventura[i] = PlayerPrefs.GetString("rankingAventura" + i);
            rankingStandard1[i] = PlayerPrefs.GetString("rankingStandard1" + i);
            rankingStandard2[i] = PlayerPrefs.GetString("rankingStandard2" + i);
            rankingStandard3[i] = PlayerPrefs.GetString("rankingStandard3" + i);
        }
    }

    private void mostrarRecords()
    {
        Text[] filas = GameObject.Find("Ranking").GetComponentsInChildren<Text>();

        if(aventura.isOn)
            for (int i = 0; i < tamaRanking; i++)
                filas[i].text = (i + 1) + ".- " + rankingAventura[i];

        else if(standard.isOn)
            if(nivel1.isOn)
                for(int i = 0; i < tamaRanking; i++)
                    filas[i].text = (i + 1) + ".- " + rankingStandard1[i];
            
            else if(nivel2.isOn)
                for (int i = 0; i < tamaRanking; i++)
                    filas[i].text = (i + 1) + ".- " + rankingStandard2[i];

            else if(nivel3.isOn)
                for (int i = 0; i < tamaRanking; i++)
                    filas[i].text = (i + 1) + ".- " + rankingStandard3[i];
    }

    private void inicializarResumen()
    {
        if(Juego.verRanking)
        {
            puntosBase.text = "PUNTUACIÓN BASE: -";
            penalizacionTiempo.text = "PENALIZACIÓN POR TIEMPO: -";
            penalizacionToques.text = "PENALIZACIÓN POR TOQUES: -;";
            vidas.text = "VIDAS: -";
            puntosFinales.text = "PUNTUACIÓN FINAL: -";
        }

        else
        {
            puntosBase.text = "PUNTUACIÓN BASE: " + Puntuacion.PUNTOS_BASE;
            penalizacionTiempo.text = "PENALIZACIÓN POR TIEMPO: -" + Puntuacion.penalizacionSegundos;
            penalizacionToques.text = "PENALIZACIÓN POR TOQUES: -" + Puntuacion.penalizacionToques;
            vidas.text = "VIDAS: " + Vidas.vidas;
            puntosFinales.text = "PUNTUACIÓN FINAL: " + Puntuacion.puntosTotales;
        }
    }

    private int posRecord(string[] ranking)
    {
        int pos = 0, n = ranking.Count(o => o != null), puntos;
        bool encontrado = false;

        while(!encontrado && pos < n)
        {
            System.Int32.TryParse(ranking[pos].Substring(ranking[pos].IndexOf("-") + 1), out puntos);

            if(puntos < Puntuacion.puntosTotales)
                encontrado = true;

            else
                pos++;
        }

        if(pos == 0 || pos < n)
            return pos;

        return -1;
    }

    private void guardarRecord()
    {
        int pos;

        if(Juego.modoAventura)
        {
            pos = this.posRecord(rankingAventura);

            if(pos > -1)   // El record ha quedado entre los 10 primeros.
            {
                // Actualizo el vector de records:

                for (int i = rankingAventura.Count(o => o != null) - 1; i > pos; i--)
                    rankingAventura[i] = rankingAventura[i - 1];

                rankingAventura[pos] = nombre.text + "-" + Puntuacion.puntosTotales;

                // Guardo los records en las preferencias:

                for(int j = 0; j < rankingAventura.Count(o => o != null); j++)
                    PlayerPrefs.SetString("rankingAventura" + j, rankingAventura[j]);
            }

            aventura.isOn = true;
        }

        else
        {
            string nivel = ElegirNivel.nivelStandard;

            switch(nivel)
            {
                case "Nivel01":
                                pos = this.posRecord(rankingStandard1);

                                if (pos > -1)   // El record ha quedado entre los 10 primeros.
                                {
                                    // Actualizo el vector de records;

                                    for (int i = rankingStandard1.Count(o => o != null) - 1; i > pos; i--)
                                        rankingStandard1[i] = rankingStandard1[i - 1];

                                    rankingStandard1[pos] = nombre.text + "-" + Puntuacion.puntosTotales;

                                    // Guardo los records en las preferencias:

                                    for (int j = 0; j < rankingStandard1.Count(o => o != null); j++)
                                        PlayerPrefs.SetString("rankingStandard1" + j, rankingStandard1[j]);
                                }

                                standard.isOn = true;
                                nivel1.isOn = true;

                                break;

                case "Nivel02":
                                pos = this.posRecord(rankingStandard2);

                                if (pos > -1)   // El record ha quedado entre los 10 primeros.
                                {
                                    // Actualizo el vector de records;

                                    for (int i = rankingStandard2.Count(o => o != null) - 1; i > pos; i--)
                                        rankingStandard2[i] = rankingStandard2[i - 1];

                                    rankingStandard2[pos] = nombre.text + "-" + Puntuacion.puntosTotales;

                                    // Guardo los records en las preferencias:

                                    for (int j = 0; j < rankingStandard2.Count(o => o != null); j++)
                                        PlayerPrefs.SetString("rankingStandard2" + j, rankingStandard2[j]);
                                }

                                standard.isOn = true;
                                nivel2.isOn = true;

                                break;

                case "Nivel03":
                                pos = this.posRecord(rankingStandard3);

                                if (pos > -1)   // El record ha quedado entre los 10 primeros.
                                {
                                    // Actualizo el vector de records;

                                    for (int i = rankingStandard3.Count(o => o != null) - 1; i > pos; i--)
                                        rankingStandard3[i] = rankingStandard3[i - 1];

                                    rankingStandard3[pos] = nombre.text + "-" + Puntuacion.puntosTotales;

                                    // Guardo los records en las preferencias:

                                    for (int j = 0; j < rankingStandard3.Count(o => o != null); j++)
                                        PlayerPrefs.SetString("rankingStandard3" + j, rankingStandard3[j]);
                                }

                                standard.isOn = true;
                                nivel3.isOn = true;

                                break;
            }
        }

        btAceptar.onClick.RemoveAllListeners();

        this.mostrarRecords();
    }

    private void limpiarRanking()
    {
        if (aventura.isOn)
            for (int i = 0; i < tamaRanking; i++)
            {
                rankingAventura[i] = "";
                PlayerPrefs.SetString("rankingAventura" + i, "");
            }

        else if (standard.isOn)
            if (nivel1.isOn)
                for (int i = 0; i < tamaRanking; i++)
                {
                    rankingStandard1[i] = "";
                    PlayerPrefs.SetString("rankingStandard1" + i, "");
                }
                    
            else if (nivel2.isOn)
                for (int i = 0; i < tamaRanking; i++)
                {
                    rankingStandard2[i] = "";
                    PlayerPrefs.SetString("rankingStandard2" + i, "");
                }

            else if (nivel3.isOn)
                for (int i = 0; i < tamaRanking; i++)
                {
                    rankingStandard3[i] = "";
                    PlayerPrefs.SetString("rankingStandard3" + i, "");
                }

        this.mostrarRecords();
    }
}

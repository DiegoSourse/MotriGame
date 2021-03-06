using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject camara = null;
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip TimeOver;
    public AudioClip Complete;
    // Start is called before the first frame update
    public TextMeshPro gameTimeText;
    public TextMeshPro score;
    //
    private int marcadas = 0;
    private int nivelActual;
    private int MaxScore;
    //
    public int InitMin = 0;
    public int InitSec = 15;
    float gameTimer = 0f;
    //
    private bool StartGame = false;
    public GameObject instrucciones = null;
    public GameObject alerta = null;
    public GameObject Abeja = null;
    //
    TextMeshProUGUI enunciado;
    TextMeshProUGUI textScore;
    TextMeshProUGUI textTiempo;
    private float porcentaje;
    private void Awake()
    {
        float estatura = EstadoJuego.estadoJuego.Estatura;
        //float estatura = 1.1f;
        porcentaje = (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        camara.transform.position = new Vector3(0f, (estatura == 0f ? 1.7f : estatura) - 0.1f, 0f);
        Abeja.transform.localScale = Abeja.transform.localScale * porcentaje;
    }
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        gameTimeText.text = "00:00";
        score.text = marcadas.ToString();
        gameTimer = (InitMin * 60) + InitSec;
        nivelActual = EstadoJuego.estadoJuego.Level;
        MaxScore = EstadoJuego.estadoJuego.MaxScorePerLevel[nivelActual];
        Debug.Log("Estamos en el nivel "+nivelActual);
        alerta.SetActive(false);
        Abeja.SetActive(false);
        //Time.timeScale = 0f;
        //Debug.Log(StartGame);
    }

    // Update is called once per frame
    void Update()
    {
        //COdigo para volver atras opcional
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("Nivel_01");
            }
        }
        //
        if (StartGame) //El juego inicia?
        {
            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            string StringTime = string.Format("{0:00}:{1:00}",minutes,seconds);
            gameTimeText.text = StringTime;
            //
            Abeja.SetActive(true);
            score.text = marcadas.ToString();
            //Debug.Log("Time:" + Time.deltaTime);
            //
            if (gameTimer>0) //El tiempo de NO juego terminó?
            {
                gameTimer -= Time.deltaTime;
                if (marcadas == MaxScore)
                {
                    MostrarAlerta("Nivel Completado...");
                    StartGame = false;
                    source.PlayOneShot(Complete);
                }
            }
            else
            {
                EstadoJuego.estadoJuego.Score = marcadas;
                EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
                MostrarAlerta("Se acabo el Tiempo...");
                source.PlayOneShot(TimeOver);
                StartGame = false;
            }
            //Faltan 10 segundo para el final?
            if (seconds <= 10 && minutes == 0)
            {
                gameTimeText.color = Color.red;
                //Reproducimos sonido de alerta
                //
            }
            //condicion para que se incremente el valor de score
            NotificationCenter.DefaultCenter.AddObserver(this, "AbejaColision");
            //condicion para que se termine el juego            
            //guardar datos
            //mostrar datos
        }        
    }
    public void AbejaColision()
    {
        marcadas++;
        EstadoJuego.estadoJuego.Score = marcadas;
        EstadoJuego.estadoJuego.Time = "00:"+gameTimeText.text;
    }
    public void IniciaJuego(bool val)
    {
        instrucciones.SetActive(false);
        StartGame = val;
        Debug.Log("Juego en Curso: "+StartGame);
    }
    public void SalirNivel()
    {
        if(EstadoJuego.estadoJuego.VerifyScoreLevel() == true)
        {
            SceneManager.LoadScene("Nivel_5");
        }else
            SceneManager.LoadScene("Nivel_01");
        //aca guardar datos almacenados
    }
    public void MostrarAlerta(string TextEnunciado)
    {
        alerta.SetActive(true);
        //Reproducir sonido
        Abeja.SetActive(false);
        //alerta.Find("Enunciado");
        //enunciado = alerta.GetComponentInChildren<TextMeshProUGUI>();
        enunciado = GameObject.Find("Enunciado").GetComponent<TextMeshProUGUI>();
        textScore = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        textTiempo = GameObject.Find("TiempoText").GetComponent<TextMeshProUGUI>();
        //
        enunciado.text = TextEnunciado;
        textScore.text = EstadoJuego.estadoJuego.Score.ToString();
        textTiempo.text = EstadoJuego.estadoJuego.Time;
        //Guardamos info de la partida
        EstadoJuego.estadoJuego.GuardarData();
    }
}

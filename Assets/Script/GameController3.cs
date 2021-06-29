using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController3 : MonoBehaviour
{
    private float porcentaje;
    private float estatura;
    #region Datos del Juego
    //TEXTO DE LA ALERTA AL FINALIZAR EL JUEGO
    TextMeshProUGUI enunciado;
    TextMeshProUGUI textScore;
    TextMeshProUGUI textTiempo;
    //AUDIO CLIP PARA FINALIZAR EL JUEGO O EL TIEMPO
    public AudioSource Source { get { return GetComponent<AudioSource>(); } }
    public AudioClip TimeOver;
    public AudioClip Complete;
    //TEXTO DE LOS CARTELES
    public TextMeshPro gameTimeText;
    public TextMeshPro score;
    //DATOS PARA EL TIEMPO DE JUEGO
    public int InitMin = 0;
    public int InitSec = 15;
    float gameTimer = 0f;
    //DATOS SOBRE EL ESTADO DEL JUEGO
    private int marcadas = 0; //CANTIDAD DE ESTRELLAS OBTENIDAS
    private int nivelActual;
    private int MaxScore;
    private bool StartGame = false;
    //CARTELES DE ALERTA E INSTRUCCIONES
    public GameObject instrucciones = null;
    public GameObject alerta = null;
    //OBJETO CAMARA A ESCALAR
    public GameObject camara;
    private GameObject RadialGaze;
    private GameObject CamataVR;
    private GameObject CamaraAR;
    //
    #endregion
    private bool ColisionSalmon = false;
    private bool ColisionStar = true;
    private Vector3 StarPosition;
    private bool splash = true;
    public GameObject Salmon;
    public GameObject Star;
    public GameObject[] croco = new GameObject[2];
    private SaltoSalmonController SaltoSalmon;
    private Vector3[] posicion = new Vector3[3];
    public float Speed = 1.25f;
    private float incremento;
    //
    private PlayerMove PlayMove;
    private void Awake()
    {
        estatura = EstadoJuego.estadoJuego.Estatura;
        //estatura = 1.07f;
        porcentaje = (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        camara.transform.position = new Vector3(camara.transform.position.x, (estatura == 0f ? 1.8f : estatura) - 0.1f, camara.transform.position.z);
        //
        //Cambia las propiedades del collider
        /*CapsuleCollider cap = camara.GetComponent<CapsuleCollider>();
        cap.height = estatura == 0 ? 1.8f : estatura;
        cap.center = new Vector3(0f, ((estatura == 0 ? 1.8f : estatura) / 2) * (-1), 0f);*/
        CharacterController car = camara.GetComponent<CharacterController>();
        car.height = estatura == 0 ? 1.8f : estatura;
        car.center = new Vector3(0f, -(((estatura == 0 ? 1.8f : estatura) / 2) - 0.1f), 0f);
        //
        PlayMove = camara.GetComponent<PlayerMove>();
        //Convocamos a los hijos de camara para deshabilitar el Gaze y la camara, activar la camara solo de vision
        //NOTA: Verificar si al cambiar de posicion se cambia tambien el collider
        CamataVR = camara.transform.GetChild(0).gameObject;
        RadialGaze = camara.transform.GetChild(1).gameObject;
        //CamaraAR = camara.transform.GetChild(2).gameObject;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        gameTimeText.text = "00:00";
        score.text = marcadas.ToString();
        gameTimer = (InitMin * 60) + InitSec;
        nivelActual = EstadoJuego.estadoJuego.Level;
        MaxScore = EstadoJuego.estadoJuego.MaxScorePerLevel[nivelActual];
        incremento = (3.0f - 1.5f) / EstadoJuego.estadoJuego.MaxScorePerLevel[nivelActual];
        Debug.Log("Estamos en el nivel " + nivelActual);
        //
        StartCoroutine("Saltar");
        StartCoroutine("StarSpawner");
        alerta.SetActive(false);
        PlayMove.enabled = false;
        //
        //CamaraAR.SetActive(false);
        //
        posicion[0] = new Vector3(0f, -1.35f, 0f);
        posicion[1] = new Vector3(2.5f, -1.35f, 0f);
        posicion[2] = new Vector3(-2.5f, -1.35f, 0f);
        //
        float StarY = (estatura == 0 ? 1.8f : estatura) * 0.603f;
        StarPosition = new Vector3(0f, StarY, 1.25f);
        ActivateCroco(false);
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
            PlayMove.enabled = true;
            //NotificationCenter.DefaultCenter.AddObserver(this, "TriggerExit");
            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            string StringTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            gameTimeText.text = StringTime;
            //
            score.text = marcadas.ToString();
            //
            //
            //Se ha colisiondo con el Salmon?
            NotificationCenter.DefaultCenter.AddObserver(this, "TriggerSalmon");
            //Jugador a colisonado con estrella?
            NotificationCenter.DefaultCenter.AddObserver(this, "TriggerStar");
            if (gameTimer > 0) //El tiempo de NO juego terminó?
            {
                gameTimer -= Time.deltaTime;
                //SE OBTUVO EL MAXIMO PUNTAJE NECESARIO??
                if (marcadas == MaxScore)
                {
                    MostrarAlerta("Nivel Completado...");
                    StartGame = false;
                    Source.PlayOneShot(Complete);
                    ActivateCroco(false);
                }
            }
            else
            {
                EstadoJuego.estadoJuego.Score = marcadas;
                EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
                MostrarAlerta("Se acabo el Tiempo...");
                Source.PlayOneShot(TimeOver);
                StartGame = false;
                ActivateCroco(false);
            }
            //Faltan 10 segundo para el final?
            if (seconds <= 10 && minutes == 0)
            {
                gameTimeText.color = Color.red;
            }
        }

    }
    //FUNCIONES DE DETECCION DE COLISION
    //con el Salmon
    void TriggerSalmon()
    {
        ColisionSalmon = true;
    }
    //con la estrella
    void TriggerStar()
    {
        ColisionStar = true;
        if(ColisionSalmon == false)
        {
            marcadas++;
            EstadoJuego.estadoJuego.Score = marcadas;
            EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
            Speed += incremento;
        }
        else
        {
            ColisionSalmon = false;
        }
    }
    void SalmonSplash()
    {
        splash = true;
    }
    //
    IEnumerator Saltar()
    {
        while (true)
        {
            if (StartGame == true && marcadas >= 1)
            {
                //splash = false;
                NotificationCenter.DefaultCenter.AddObserver(this,"SalmonSplash");
                if(splash == true)
                {
                    splash = false;
                    int Nro = Random.Range(1, 3);
                    GameObject proye = Instantiate(Salmon, posicion[Nro], transform.rotation);
                    SaltoSalmon = proye.GetComponentInChildren<SaltoSalmonController>();
                    //proy = proye.GetComponent<Proyectil>();
                    float Altura = Random.Range(1.25f + 0.5f, 1.25f + (estatura - 0.22f));
                    SaltoSalmon.arcHeight = Altura;
                    //Debug.Log(SaltoSalmon.arcHeight);
                    SaltoSalmon.speed = Speed;
                    SaltoSalmon.Eje = 'x';
                    if (Nro == 1)
                    {
                        SaltoSalmon.startPos = posicion[1];
                        SaltoSalmon.targetPos = posicion[2];
                    }
                    if (Nro == 2)
                    {
                        SaltoSalmon.startPos = posicion[2];
                        SaltoSalmon.targetPos = posicion[1];
                    }
                    yield return new WaitForSeconds(2f);
                    SaltoSalmon.salto = true;
                }
            }
            yield return null;
        }
    }
    //
    IEnumerator StarSpawner()
    {
        while (true)
        {
            if (StartGame == true)
            {
                if(ColisionStar == true)
                {
                    GameObject[] estrella = GameObject.FindGameObjectsWithTag("star");
                    yield return new WaitForSeconds(0.5f);
                    foreach (var item in estrella)
                    {
                        Destroy(item);
                    }
                    Instantiate(Star, StarPosition, transform.rotation);
                    ColisionStar = false;
                    StarPosition.z = StarPosition.z * (-1);
                }                
            }
            yield return null;
        }
    }
    void ActivateCroco(bool estado)
    {
        foreach (var item in croco)
        {
            item.SetActive(estado);
        }
    }
    //FUNCIONES QUE DEBEN ESTAR EN LOS DEMAS GAMECONTROLLER
    public void IniciaJuego(bool val)
    {
        //Funcion ButtonPress que permite habilitar el Juego
        instrucciones.SetActive(false);
        StartGame = val;
        //
        RadialGaze.SetActive(false);
        //CamataVR.SetActive(false);
        //CamaraAR.SetActive(true);
        //
        ActivateCroco(true);
        Debug.Log("Juego en Curso: " + StartGame);
    }
    public void SalirNivel()
    {
        //SceneManager.LoadScene("Nivel_01");
        if (EstadoJuego.estadoJuego.VerifyScoreLevel() == true)
        {
            SceneManager.LoadScene("Nivel_5");
        }
        else
            SceneManager.LoadScene("Nivel_01");
    }
    public void MostrarAlerta(string TextEnunciado)
    {
        alerta.SetActive(true);
        RadialGaze.SetActive(true);
        //CamataVR.SetActive(true);
        //CamaraAR.SetActive(false);
        //
        GameObject star = GameObject.FindGameObjectWithTag("star");
        if (star != null)
        {
            Destroy(star);
        }
        PlayMove.enabled = false;
        camara.transform.position = new Vector3(0.0f, camara.transform.position.y, 0.0f);
        //
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController4 : MonoBehaviour
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
    public GameObject Salmon;
    public GameObject Star;
    private SaltoSalmonController SaltoSalmon;
    public float Speed = 1.25f;
    private float incremento;
    private bool splash = true;
    //
    private bool ColisionSalmon = false;
    private bool ColisionStar = true;
    //
    private Vector3[] SalmonPosicion = new Vector3[5];
    private char[] Eje = {'n','z','x','z','x' };
    //private float[] CoorDes = {0f,-2f,-3f,3f,3.5f };

    private Vector3[] StarPosicion = new Vector3[11];
    private string[] Vecinos = {
        "[0][1][2][3]",
        "[1][0][2][4][5]",
        "[2][0][1][3][4][5][6]",
        "[3][0][2][5][6]",
        "[4][1][2][5][7][8]",
        "[5][1][2][3][4][6][7][8][9]",
        "[6][2][3][5][8][9]",
        "[7][4][5][8][10]",
        "[8][4][5][6][7][9][10]",
        "[9][5][6][8][10]",
        "[10][7][8][9]"
    };
    private int Anterior;
    //
    private PlayerMove PlayMove;
    //
    private int Nro;
    public GameObject Linea;
    private void Awake()
    {
        //estatura = EstadoJuego.estadoJuego.Estatura;
        estatura = 1.0f;
        porcentaje = (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        camara.transform.position = new Vector3(camara.transform.position.x, (estatura == 0f ? 1.8f : estatura) - 0.1f, camara.transform.position.z);
        //Cambia las propiedades del collider
        /*CapsuleCollider cap= camara.GetComponent<CapsuleCollider>();
        cap.height = estatura == 0 ? 1.8f : estatura;
        cap.center = new Vector3(0f, ((estatura == 0 ? 1.8f : estatura) / 2) * (-1), 0f);*/
        CharacterController car = camara.GetComponent<CharacterController>();
        car.height = estatura == 0 ? 1.8f : estatura;
        car.center = new Vector3(0f, -(((estatura == 0 ? 1.8f : estatura) / 2) - 0.1f), 0f);
        //
        PlayMove = camara.GetComponent<PlayerMove>();
        //
        //Convocamos a los hijos de camara para deshabilitar el Gaze y la camara, activar la camara solo de vision
        CamataVR = camara.transform.GetChild(0).gameObject;
        RadialGaze = camara.transform.GetChild(1).gameObject;
        //CamaraAR = camara.transform.GetChild(2).gameObject;
        float StarY = (estatura == 0 ? 1.8f : estatura) * 0.603f;

        StarPosicion[0] = new Vector3(0f, StarY, -0.875f);
        StarPosicion[1] = new Vector3(-0.5f, StarY, -0.5f);
        StarPosicion[2] = new Vector3(0f, StarY, -0.5f);
        StarPosicion[3] = new Vector3(0.5f, StarY, -0.5f);
        StarPosicion[4] = new Vector3(-0.5f, StarY, 0f);
        StarPosicion[5] = new Vector3(0f, StarY, 0f);
        StarPosicion[6] = new Vector3(0.5f, StarY, 0f);
        StarPosicion[7] = new Vector3(-0.5f, StarY, 0.5f);
        StarPosicion[8] = new Vector3(0f, StarY, 0.5f);
        StarPosicion[9] = new Vector3(0.5f, StarY, 0.5f);
        StarPosicion[10] = new Vector3(0f, StarY, 0.875f);
    
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
        //
        incremento = (3.0f - 1.25f) / EstadoJuego.estadoJuego.MaxScorePerLevel[nivelActual];
        //incremento = (3.0f - 1.25f) / EstadoJuego.estadoJuego.MaxScorePerLevel[4];
        Debug.Log("Estamos en el nivel " + nivelActual);
        //
        alerta.SetActive(false);
        //
        //CamaraAR.SetActive(false);
        //
        PlayMove.enabled = false;
        SalmonPosicion[0] = new Vector3(0f, -0.5f, 0f);
        SalmonPosicion[1] = new Vector3(0f, -0.5f, 2.25f);//z
        SalmonPosicion[2] = new Vector3(2.25f, -0.5f, 0f);//x
        SalmonPosicion[3] = new Vector3(0f, -0.5f, -2.25f);//-z
        SalmonPosicion[4] = new Vector3(-2.25f, -0.5f, 0f);//-x
        //
        Nro = 1;
        Anterior = 0;
        StartCoroutine("Saltar");
        StartCoroutine("StarSpawner");
        //
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
            GeneraLinea();
            //NotificationCenter.DefaultCenter.AddObserver(this, "TriggerExit");
            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            string StringTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            gameTimeText.text = StringTime;
            //
            score.text = marcadas.ToString();
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
                }
            }
            else
            {
                EstadoJuego.estadoJuego.Score = marcadas;
                EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
                MostrarAlerta("Se acabo el Tiempo...");
                Source.PlayOneShot(TimeOver);
                StartGame = false;
            }
            //Faltan 10 segundo para el final?
            if (seconds <= 10 && minutes == 0)
            {
                gameTimeText.color = Color.red;
            }
        }

    }
    //
    void SalmonSplash() //Salmon toca el agua?
    {
        splash = true;
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
        if (ColisionSalmon == false)
        {
            marcadas++;
            EstadoJuego.estadoJuego.Score = marcadas;
            EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
            //Speed += incremento;
        }
        else
        {
            ColisionSalmon = false;
        }
    }
    IEnumerator Saltar()
    {
        //PODEMOS COLOCARLO EN EL START PARA PODER DETERMINAR DONDE SALDRÁ EL SALMON
        while (true)
        {
            if (StartGame == true && marcadas >= 1)
            {
                //splash = false;
                NotificationCenter.DefaultCenter.AddObserver(this, "SalmonSplash");
                if (splash == true)
                {
                    splash = false;
                    //Nro = Random.Range(1, 5);
                    GameObject proye = Instantiate(Salmon, SalmonPosicion[Nro], transform.rotation);
                    Nro++;
                    if (Nro>4)
                    {
                        Nro = 1;
                    }
                    SaltoSalmon = proye.GetComponentInChildren<SaltoSalmonController>();
                    float Altura = 0.5f + (estatura - 0.15f);
                    SaltoSalmon.arcHeight = Altura;
                    //Debug.Log(SaltoSalmon.arcHeight+" Estatura:"+estatura);
                    SaltoSalmon.speed = Speed;
                    SaltoSalmon.Eje = Eje[Nro];
                    //
                    SaltoSalmon.startPos = SalmonPosicion[Nro];
                    if (Eje[Nro] == 'z')
                        SaltoSalmon.targetPos = new Vector3(SalmonPosicion[Nro].x, SalmonPosicion[Nro].y, -SalmonPosicion[Nro].z);
                    else if (Eje[Nro] == 'x')
                        SaltoSalmon.targetPos = new Vector3(-SalmonPosicion[Nro].x, SalmonPosicion[Nro].y, SalmonPosicion[Nro].z);
                    //
                    yield return new WaitForSeconds(3f);
                    SaltoSalmon.salto = true;
                }
            }
            yield return null;
        }
    }
    //
    bool ValidarAleatorio(int anterior, int nuevo)
    {
        if(nuevo != anterior)
        {
            string cadena = "[" + nuevo + "]";
            if (Vecinos[anterior].Contains(cadena))
                return false;
            else
                return true;
        }
        return false;        
    }
    //
    IEnumerator StarSpawner()
    {
        while (true)
        {
            if (StartGame == true)
            {
                if (ColisionStar == true)
                {
                    GameObject estrella = GameObject.FindGameObjectWithTag("star");
                    if (estrella != null)
                    {
                        yield return new WaitForSeconds(0.5f);
                        Destroy(estrella);
                    }
                    int Nuevo = Random.Range(0, 11);
                    //Debug.Log(Nuevo);
                    if (ValidarAleatorio(Anterior, Nuevo))
                    {
                        //Debug.Log("ESTE SI: "+Nuevo);
                        Anterior = Nuevo;
                        Instantiate(Star, StarPosicion[Nuevo], transform.rotation);
                        //GeneraLinea(StarPosicion[Nuevo]);
                        ColisionStar = false;
                        //StarPosition.z = StarPosition.z * (-1);
                    }
                }
            }
            yield return null;
        }
    }
    void GeneraLinea()
    {
        //
        GameObject star = GameObject.FindGameObjectWithTag("star");
        if (star != null)
        {
            Vector3 des = star.transform.position;
            LineRenderer line = Linea.GetComponent<LineRenderer>();
            Vector3 origen = new Vector3(camara.transform.position.x, 0.05f, camara.transform.position.z);
            Vector3 destino = new Vector3(des.x, 0.05f, des.z);
            line.SetPosition(0, origen);
            line.SetPosition(1, destino);
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
        GameObject star = GameObject.FindGameObjectWithTag("star");
        if (star != null)
        {
            Destroy(star);
        }
        //CamataVR.SetActive(true);
        //CamaraAR.SetActive(false);
        //
        PlayMove.enabled = false;
        camara.transform.position = new Vector3(0.0f, camara.transform.position.y, -0.875f);
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

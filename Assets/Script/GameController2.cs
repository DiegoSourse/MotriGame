using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController2 : MonoBehaviour
{
    #region Valores de Poses
    private string[] poses = {  //sentado
                                "1,2,3,4,5,6,7,8,9,12,13,16",
                                "1,2,3,4,5,6,7,8,9,13,16",
                                "1,2,3,4,5,6,7,8,12,13,16",
                                //arrodillado
                                "1,2,3,4,5,8,9,12,13,16",
                                "1,2,3,4,5,9,12,13,16",
                                "1,2,3,4,8,9,12,13,16",
                                //parado centro
                                "1,4,5,8,9,12,13,16",
                                "1,4,5,8,9,12,13,14,16",
                                "1,4,5,8,9,12,13,15,16",
                                "5,8,9,12,13,16",
                                "5,8,9,12,13,14,16",
                                "5,8,9,12,13,15,16",
                                "4,5,8,9,12,13,16",
                                "4,5,8,9,12,13,14,16",
                                "4,5,8,9,12,13,15,16",
                                "1,5,8,9,12,13,16",
                                "1,5,8,9,12,13,14,16",
                                "1,5,8,9,12,13,15,16",
                                //parado derecha
                                "3,4,7,8,11,12,15,16",
                                "3,4,7,8,11,12,13,15,16",
                                "3,4,7,8,11,12,14,15,16",
                                "4,7,8,11,12,15,16",
                                "4,7,8,11,12,13,15,16",
                                "4,7,8,11,12,14,15,16",
                                //parado izquierda
                                "1,2,5,6,9,10,13,14",
                                "1,2,5,6,9,10,13,14,15",
                                "1,2,5,6,9,10,13,14,16",
                                "1,5,6,9,10,13,14",
                                "1,5,6,9,10,13,14,15",
                                "1,5,6,9,10,13,14,16"
    };
    private Condicion[] condiciones = {
        //Sentado
        new Condicion("SentadoEspera",1,false,false),new Condicion("SentadoBI",1,false,true),new Condicion("SentadoBD",1,true,false),
        //Arrodillado
        new Condicion("ArrodilladoEspera",2,false,false),new Condicion("ArrodilladoBI",2,false,true),new Condicion("ArrodilladoBD",2,true,false),
        //Parado
        new Condicion("Parado",3,false,false),new Condicion("ParadoPD",3,true,false),new Condicion("ParadoPI",3,false,true),
        new Condicion("ParadoExt",4,false,false),new Condicion("ParadoExtPD",4,true,false),new Condicion("ParadoExtPI",4,false,true),
        new Condicion("ParadoBDExt",5,false,false),new Condicion("ParadoBDExtPD",5,true,false),new Condicion("ParadoBDExtPI",5,false,true),
        new Condicion("ParadoBIExt",6,false,false),new Condicion("ParadoBIExtPD",6,true,false),new Condicion("ParadoBIExtPI",6,false,true),
        //Parado Derecha        
        new Condicion("Parado",3,false,false),new Condicion("ParadoPD",3,true,false),new Condicion("ParadoPI",3,false,true),
        new Condicion("ParadoBIExt",6,false,false),new Condicion("ParadoBIExtPD",6,true,false),new Condicion("ParadoBIExtPI",6,false,true),
        //Parado Izquierda
        new Condicion("Parado",3,false,false),new Condicion("ParadoPD",3,true,false),new Condicion("ParadoPI",3,false,true),
        new Condicion("ParadoBDExt",5,false,false),new Condicion("ParadoBDExtPD",5,true,false),new Condicion("ParadoBDExtPI",5,false,true),
    };
    private string[] EstadosEnt = { "Ninguno", "SentadoEspera", "ArrodilladoEspera", "Parado", "ParadoExt", "ParadoBDExt", "ParadoBIExt", "Animo", "Saludo" };
    #endregion
    #region Valores del Jugador
    private float porcentaje;
    const float punto = 0.445f;
    private float PuntoSalto;
    #endregion
    #region Valores el Juego
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    //public AudioClip Star;
    public AudioClip TimeOver;
    public AudioClip Complete;
    //
    public TextMeshPro gameTimeText;
    public TextMeshPro score;
    public int InitMin = 0;
    public int InitSec = 15;
    float gameTimer = 0f;
    //
    private int marcadas = 0; //CANTIDAD DE ESTRELLAS OBTENIDAS
    private int nivelActual;
    private int MaxScore;
    //
    TextMeshProUGUI enunciado;
    TextMeshProUGUI textScore;
    TextMeshProUGUI textTiempo;
    //
    private bool StartGame = false;
    private bool ExitTronco = true;
    public GameObject instrucciones = null;
    public GameObject alerta = null;
    //
    public GameObject camara;
    public GameObject Ent;
    public GameObject Base;
    private Animator anim = new Animator();
    private SaltoEntController SaltoEnt;
    //
    private int NroPose;
    private int EstadoActual = 0;
    private bool Animar = false;
    private bool EstaSaltando = false;
    public float VelocidadTronco = 1.5f;
    private float Decremento;
    private bool TroncoColision = false;
    //
    private Vector3 Destino;
    //
    private SpawnTronco spawn;
    private SpawnStar estrella;
    #endregion
    //
    const float AltoTronco = 0.386f;
    const float EspacioSuelo = 0.099f;
    private float N_Alto, Sentado, Arrodillado;
    private PlayerMove2 player;
    //
    private void Awake()
    {
        float estatura = EstadoJuego.estadoJuego.Estatura;
        //float estatura = 1.07f;
        porcentaje= (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        PuntoSalto = punto * porcentaje;
        camara.transform.position = new Vector3(camara.transform.position.x, (estatura == 0f ? 1.8f : estatura) - 0.1f, camara.transform.position.z);
        //escalamos al ent
        //Ent.transform.localScale = new Vector3(Ent.transform.localScale.x*porcentaje, Ent.transform.localScale.y*porcentaje, Ent.transform.localScale.z*porcentaje);
        //Escalamos el collider o character
        CapsuleCollider cap = camara.GetComponent<CapsuleCollider>();
        float valor = (estatura == 0 ? 1.8f : estatura) - (0.099f * porcentaje) - (1.5f * (0.386f * porcentaje));
        cap.height = valor;
        cap.center = new Vector3(0f, -((valor / 2) - 0.05f), 0f);
        cap.radius = cap.radius * porcentaje;
        //
        Ent.transform.localScale = Ent.transform.localScale * porcentaje;
        //Escalamos la base
        GameObject baseEnt = Base.transform.GetChild(0).gameObject;
        GameObject basePlayer = Base.transform.GetChild(1).gameObject;
        baseEnt.transform.localScale = baseEnt.transform.localScale * porcentaje;
        basePlayer.transform.localScale = basePlayer.transform.localScale * porcentaje;
        //Ent.transform.localScale
        spawn = FindObjectOfType <SpawnTronco>();
        estrella = FindObjectOfType<SpawnStar>();
        anim = Ent.GetComponentInChildren<Animator>();
        //
        N_Alto = AltoTronco * porcentaje;
        Sentado = (N_Alto * 2) + EspacioSuelo - 0.1f;
        Arrodillado = (N_Alto * 3) + EspacioSuelo - 0.1f;
        //
        player = camara.GetComponent<PlayerMove2>();
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
        Debug.Log("Estamos en el nivel " + nivelActual);
        //
        SaltoEnt = Ent.GetComponent<SaltoEntController>();
        Destino = new Vector3(0f,0f,0f);
        //SaltoEnt.TargetPos = Destino;
        SaltoEnt.Speed = 0.5f;
        SaltoEnt.ArcHeight = PuntoSalto/2;
        //
        alerta.SetActive(false);
        Ent.SetActive(false);
        StartCoroutine(GenerarPose());
        //
        Decremento = (VelocidadTronco - 0.8f) / 25;
        //
        player.llegada = true;
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
            //NotificationCenter.DefaultCenter.AddObserver(this, "TriggerExit");
            int seconds = (int)(gameTimer % 60);
            int minutes = (int)(gameTimer / 60) % 60;
            string StringTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            gameTimeText.text = StringTime;
            //
            Ent.SetActive(true);
            score.text = marcadas.ToString();
            //
            if (gameTimer > 0) //El tiempo de NO juego terminó?
            {
                //El observado verifica si hubo colision de Star y Player
                NotificationCenter.DefaultCenter.AddObserver(this,"TriggerPlayer");
                NotificationCenter.DefaultCenter.AddObserver(this, "TriggerStar");
                if (EstaSaltando == true)
                {
                    SaltoEnt.TargetPos = Destino;
                    EstaSaltando = false;
                    SaltoEnt.Salto = true;
                    anim.SetBool("saltar", true);
                }
                if (Animar == true && Ent.transform.position.x == SaltoEnt.TargetPos.x)
                {
                    AnimarPersonaje(NroPose);
                    Animar = false;
                }
                if(ExitTronco == true && anim.GetCurrentAnimatorStateInfo(0).IsName(EstadosEnt[EstadoActual]))
                {
                    anim.SetInteger("estado", 3);
                }
                if(Ent.transform.position.x == SaltoEnt.TargetPos.x && anim.GetCurrentAnimatorStateInfo(0).IsName("Salto"))
                {
                    anim.SetBool("saltar", false);
                }
                gameTimer -= Time.deltaTime;
                //SE OBTUVO EL MAXIMO PUNTAJE NECESARIO??
                if (marcadas == MaxScore)
                {
                    MostrarAlerta("Nivel Completado...");
                    StartGame = false;
                    source.PlayOneShot(Complete);
                    DestruirObjetos();
                }
            }
            else
            {
                EstadoJuego.estadoJuego.Score = marcadas;
                EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
                MostrarAlerta("Se acabo el Tiempo...");
                source.PlayOneShot(TimeOver);
                StartGame = false;
                DestruirObjetos();
            }
            //Faltan 10 segundo para el final?
            if (seconds <= 10 && minutes == 0)
            {
                gameTimeText.color = Color.red;
            } 
        }
    }
    void TriggerExit()
    {
        ExitTronco = true;
        TroncoColision = false;
        DefaultState();
    }
    void TriggerPlayer()
    {
        TroncoColision = true;
    }
    void TriggerStar()
    {
        if (TroncoColision == false)
        {
            VelocidadTronco -= Decremento;
            marcadas = marcadas + 1;
            EstadoJuego.estadoJuego.Score = marcadas;
            EstadoJuego.estadoJuego.Time = "00:" + gameTimeText.text;
        }
        else
            TroncoColision = false;
    }
    void DestruirObjetos()
    {
        GameObject[] Troncos = GameObject.FindGameObjectsWithTag("proyectil");
        foreach (var item in Troncos)
        {
            Destroy(item);
        }
    }
    IEnumerator GenerarPose()
    {
        while (true)
        {
            if (StartGame==true)
            {
                NotificationCenter.DefaultCenter.AddObserver(this, "TriggerExit");
                if (ExitTronco == true)
                {
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Parado") && Ent.transform.position.x == 0f && player.llegada == true && player.volver == false) //Ya esta parado y esta en el centro?
                    {
                        ExitTronco = false;
                        //
                        NroPose = Random.Range(0, 30);  //Generamos un numero para os troncos
                        //NroPose = 29;
                        Debug.Log(NroPose+":"+condiciones[NroPose].Nombre);
                        player.opcion = 0;
                        yield return new WaitForSeconds(1);
                        //Preguntar si NroPose es 18-23 DERECHA
                        if (NroPose > 17)
                        {
                            player.llegada = false;
                            EstaSaltando = true;
                            if (NroPose > 23)
                            {
                                //Condicion para Movimiento DERECHA
                                Destino.x = PuntoSalto;
                                player.DestinoX = PuntoSalto;
                                player.opcion = 1;
                            }
                            else
                            {
                                //Condicion para Movimiento IZQUIERDA
                                Destino.x = PuntoSalto * (-1);
                                player.DestinoX = -PuntoSalto;
                                player.opcion = 2;
                            }
                        }
                        else
                        {
                            //condicion de parado o arrodillado
                            if(NroPose < 6)
                            {
                                player.llegada = false;
                                player.opcion = 4;
                                if(NroPose < 3) {   player.DestinoY = Sentado;  }
                                else {   player.DestinoY = Arrodillado; }
                            }
                        }

                        yield return new WaitForSeconds(1); //Esperamos tiempo antes de Generar los troncos
                        int[] pos = ObtenerPose(NroPose);
                        spawn.GeneraPoseProyectil(pos);
                        estrella.GeneraStar(NroPose);
                        Animar=true;
                        //
                        //yield return new WaitForSeconds(2); //Esperamos tiempo antes de que empiecen a moverse
                    }
                    //Salio el tronco
                    else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Parado") && (Ent.transform.position.x == PuntoSalto || Ent.transform.position.x == -PuntoSalto) && EstaSaltando == false)
                    {
                        Destino.x = 0f;
                        EstaSaltando = true;
                    }
                }
                else 
                if(anim.GetCurrentAnimatorStateInfo(0).IsName(condiciones[NroPose].Nombre) && player.llegada == true) //ACA PODEMOS AÑADIR LA CONDICIÓN DE LLEGADA DE PLAYER
                {
                    spawn.AplicarVelocidad(VelocidadTronco);
                    estrella.AplicarVelocidad(VelocidadTronco);
                }
            }
            yield return null;
        }
    }
    public int[] ObtenerPose(int i)
    {
        string[] pose = poses[i].Split(',');
        int[] ints = System.Array.ConvertAll(pose, int.Parse);
        return ints;
    }
    ///
    void AnimarPersonaje(int pose)
    {
        int est = condiciones[pose].Estado;
        bool der = condiciones[pose].Derecha;
        bool izq = condiciones[pose].Izquierda;
        //
        Debug.Log(condiciones[pose].Data());
        //
        anim.SetInteger("estado",est);
        anim.SetBool("derecha",der);
        anim.SetBool("izquierda",izq);
        //
        EstadoActual = anim.GetInteger("estado");
    }
    void DefaultState()
    {
        anim.SetInteger("estado", EstadoActual);
        anim.SetBool("derecha", false);
        anim.SetBool("izquierda", false);
    }
    //FUNCIONES QUE DEBEN ESTAR EN LOS DEMAS GAMECONTROLLER
    public void IniciaJuego(bool val)
    {
        instrucciones.SetActive(false);
        StartGame = val;
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
        Ent.SetActive(false);
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
//Colocar una variable Nombre, la cual álmacenará el nombre del estado, para determinar si esta en ese estado para luego iniciar con el movimiento
//Luego verificar si todos lo movimientos son correctos
public class Condicion
{
    public string Nombre { get; set; }
    public int Estado { get; set; }
    public bool Derecha { get; set; }
    public bool Izquierda { get; set; }
    public Condicion(string nombre,int estado,bool der, bool izq)
    {
        Nombre = nombre;
        Estado = estado;
        Derecha = der;
        Izquierda = izq;
    }
    public string Data()
    {
        return "Estado: " + Estado + " -- Derecha: " + Derecha + " -- Izquierda: " + Izquierda;
    }
}

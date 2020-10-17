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
        new Condicion(1,false,false),new Condicion(1,false,true),new Condicion(1,true,false),
        //Arrodillado
        new Condicion(2,false,false),new Condicion(2,false,true),new Condicion(2,true,false),
        //Parado
        new Condicion(3,false,false),new Condicion(3,true,false),new Condicion(3,false,true),
        new Condicion(4,false,false),new Condicion(4,true,false),new Condicion(4,false,true),
        new Condicion(5,false,false),new Condicion(5,true,false),new Condicion(5,false,true),
        new Condicion(6,false,false),new Condicion(6,true,false),new Condicion(6,false,true),
        //Parado Derecha        
        new Condicion(3,false,false),new Condicion(3,true,false),new Condicion(3,false,true),
        new Condicion(6,false,false),new Condicion(6,true,false),new Condicion(6,false,true),
        //Parado Izquierda
        new Condicion(3,false,false),new Condicion(3,true,false),new Condicion(3,false,true),
        new Condicion(5,false,false),new Condicion(5,true,false),new Condicion(5,false,true),
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
    private int marcadas = 0;
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
    private Animator anim = new Animator();
    //
    public int NroPose;
    private int EstadoActual = 0;
    private bool Animar = false;
    //
    //public GameObject spawner;
    private SpawnTronco spawn;
    //public GameObject gameRegion;
    /*public int estado = 0;
    public bool derecha = false;
    public bool izquierda = false;*/
    //public bool salto = false;
    //public GameObject ent;
    //private Animator anim=new Animator();
    //
    //public Animator anim;
    //public Rigidbody rb;
    #endregion
    //
    private void Awake()
    {
        //NroPose = 0;
        float estatura = EstadoJuego.estadoJuego.Estatura;
        porcentaje= (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        PuntoSalto = punto * porcentaje;
        camara.transform.position = new Vector3(camara.transform.position.x, estatura == 0f ? 1.6f : estatura, camara.transform.position.z);
        //escalamos al ent
        Ent.transform.localScale = new Vector3(Ent.transform.localScale.x*porcentaje, Ent.transform.localScale.y*porcentaje, Ent.transform.localScale.z*porcentaje);
        //Ent.transform.localScale
        spawn = FindObjectOfType <SpawnTronco>();
        anim = Ent.GetComponent<Animator>();
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
        alerta.SetActive(false);
        Ent.SetActive(false);
        StartCoroutine(GenerarPose());
    }
    // Update is called once per frame
    void Update()
    {
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
            //Debug.Log("Time:" + Time.deltaTime);
            //
            if (gameTimer > 0) //El tiempo de NO juego terminó?
            {
                if(Animar==true)
                {
                    AnimarPersonaje(NroPose);
                    Animar = false;
                }
                if(ExitTronco == true && anim.GetCurrentAnimatorStateInfo(0).IsName(EstadosEnt[EstadoActual]))
                {
                    anim.SetInteger("estado", 3);
                }
                //
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
            } 
        }
        /*
        //anim.SetTrigger("salto");
        anim.SetInteger("estado",estado);
        //anim.SetBool("derecha",derecha);
        //anim.SetBool("izquierda", izquierda);
        if (salto)
        {
            rb.AddForce(Vector3.up * 800, ForceMode.Force);
            //rb.AddForce(Vector3.right * 100, ForceMode.Force);
            anim.SetFloat("salto", rb.transform.position.y);
            
        }
        anim.SetFloat("salto", rb.transform.position.y);
        */

    }
    void TriggerExit()
    {
        ExitTronco = true;
        //
        DefaultState();
        //StartCoroutine(Tempo());
        //anim.SetInteger("estado", 3);
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
                    //DefaultState();
                    //anim.SetInteger("estado", 3);
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("Parado"))
                    {
                        ExitTronco = false;
                        //int NroPose = Random.Range(0, 18);  //Generamos un numero para os troncos
                        //int NroPose = 0;
                        Debug.Log("POSE: " + NroPose);
                        yield return new WaitForSeconds(1); //Esperamos tiempo antes de Generar los troncos
                        int[] pos = ObtenerPose(NroPose);
                        spawn.GeneraPoseProyectil(pos);
                        //AnimarPersonaje(NroPose);
                        Animar=true;
                        //
                        yield return new WaitForSeconds(2); //Esperamos tiempo antes de que empiecen a moverse
                        //spawn.AplicarVelocidad(1.5f);
                        //Debug.Log("HOLAAAAAA");
                        //NroPose++;
                    }
                }
                else
                    spawn.AplicarVelocidad(1.5f);
            }
            yield return null;
        }
    }
    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(0.5f);
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
        //StartCoroutine(Tempo());
        //anim.SetInteger("estado", 3);
        /*if (state > 17)
        {
            //aca hacemos que vuelva a su posicion de origen si es mayor a 17
        }   */
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
        //EstadoJuego.estadoJuego.GuardarData();
    }
    void Imprimir()
    {
        for (int i = 0; i < condiciones.Length; i++)
        {
            Debug.Log(condiciones[i].Data());
        }
        for (int i = 0; i < poses.Length; i++)
        {
            Debug.Log(poses[i]);
        }
    }
}
//Colocar una variable Nombre, la cual álmacenará el nombre del estado, para determinar si esta en ese estado para luego iniciar con el movimiento
//Luego verificar si todos lo movimientos son correctos
public class Condicion
{
    public int Estado { get; set; }
    public bool Derecha { get; set; }
    public bool Izquierda { get; set; }
    public Condicion(int estado,bool der, bool izq)
    {
        Estado = estado;
        Derecha = der;
        Izquierda = izq;
    }
    public string Data()
    {
        return "Estado: " + Estado + " -- Derecha: " + Derecha + " -- Izquierda: " + Izquierda;
    }
}

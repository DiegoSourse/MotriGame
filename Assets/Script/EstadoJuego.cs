using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region packageSQLite
using System.Data;
#endregion

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;
    // Start is called before the first frame update
    #region UserData
        private int idPlayer;
        private string nombrePlayer;
        private string aliasPlayer;
        private float estaturaPlayer;
        private int edadPlayer;
    #endregion
    #region LevelData
        public int LevelSelected;
        public bool[] NivelJugado;
        private int previousLevel;
        private int level;
        //private string time;
        private int score;
        public int[] ScoreLevel;
    //private string fecha;
    #endregion
    //private string totalTime;
    private int totalScore = 0;
    //

    #region DatabaseVariables
        private string conn, sqlQuery;
        IDbConnection dbconn;
        IDbCommand dbcmd;
        //private IDataReader reader;
    #endregion
    SQLiteAndroid sql;
    private void Awake()
    {
        /*Debug.Log("Nivel actual: " + level);
        Debug.Log("Nivel previo: " + previousLevel);
        Debug.Log("Total score: " + totalScore);*/
        //NivelJugado[1] = true;
        NivelJugado =  new bool[5];
        ScoreLevel = new int[] {0,0,0,0,0};

        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this)
        {
            Destroy(gameObject);
        }
        /*System.DateTime dateTime = System.DateTime.Now;
        Debug.Log(dateTime.ToString("yyyy-MM-dd"));*/
    }
    void Start()
    {
        //NivelJugado[1] = true;
        //Sirve para enviar mensaje entre escenas sin necesidad de acceder al objeto
        //NotificationCenter.DefaultCenter.AddObserver(this,"");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public string Time { get; set; }
    public int GetId()
    {
        return idPlayer;
    }
    public void SetId(int newId)
    {
        idPlayer = newId;
    }
    public string GetAlias()
    {
        return aliasPlayer;
    }
    public void SetAlias(string newAlias)
    {
        aliasPlayer = newAlias;
    }
    //
    public int Getlevel()
    {
        return level;
    }
    public void Setlevel(int newLevel)
    {
        level = newLevel;
    }
    //
    public int GetPrevLevel()
    {
        return previousLevel;
    }
    public void SetPrevLevel(int prev)
    {
        previousLevel = prev;
    }
    //
    public int GetTotalScore()
    {
        return totalScore;
    }
    //
    public float Estatura
    {
        get { return estaturaPlayer;}
        set { estaturaPlayer = value; }
    }
    public int Edad
    {
        get { return edadPlayer; }
        set { edadPlayer = value; }
    }
    public string Nombre
    {
        get { return nombrePlayer; }
        set { nombrePlayer = value; }
    }
    //
    public void RestoreValues()
    {
        //NivelJugado =new bool[] { false, false, false, false, false };
        idPlayer = 0;
        aliasPlayer = "";
        LevelSelected = 0;
        previousLevel = 0;
        level = 0;
        //time;
        score = 0;
        //fecha;
        //totalTime = "";
        totalScore = 0;
    }
    public void GuardarData()
    {
        //preguntamos si el puntaje obtenido en el Nivel es mayor al que esta almacenado en EstadoJuego
        if(score >= ScoreLevel[level])
        {
            ScoreLevel[level] = score;
        }
        totalScore = ScoreLevel[1] + ScoreLevel[2] + ScoreLevel[3] + ScoreLevel[4];
        System.DateTime dateTime = System.DateTime.Now;
        string fecha = dateTime.ToString("yyyy-MM-dd");
        string query = "INSERT INTO Registro (Id_jugador,Fecha,Nivel,Puntaje,Tiempo) " +
            "VALUES("+idPlayer+",'"+fecha+"',"+level+","+score+",'"+Time+"');";
        sql.Conectar();
        sql.insert_function(query);
        if (ScoreLevel[level] == 25)
        {
            NivelJugado[level] = true;
        }
        //Considero que deberia verificase si se logro algun logro cuando se almacena el registro de la partida
        //tanto individual como global
        //Preguntamos si el score y nivel estan presentes en la tabla de logros
        //luego si la suma de score esta en la tabla
        //caso afirmativo en cualquier caso se añade a la tabla de Jugador_Logro
    }
    //verificacmos si se obtuvo algun logro
    public void VerificaLogro()
    {

    }
}

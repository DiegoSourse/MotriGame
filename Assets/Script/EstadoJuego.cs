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
    //private int idPlayer;
    //private string aliasPlayer;
    public int IdPlayer { get; set; }
    public string AliasPlayer { get; set; }
    public float Estatura { get; set; }
    public int Edad { get; set; }
    public string Nombre { get; set; }
    //
    public string Time { get; set; }
    public int Score { get; set; }
    #endregion
    #region LevelData
    public int LevelSelected;
    public bool[] NivelJugado;
    //private int previousLevel;
    //private int level;
    public int Level { get; set; }
    public int PrevLevel { get; set; }
    public int[] ScoreLevel;
    //private string fecha;
    #endregion
    public int[] MaxScorePerLevel = { 100, 25, 25, 25, 25 };
    //private string totalTime;
    private int totalScore = 0;
    //
    //
    #region DatabaseVariables
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    //private IDataReader reader;
    #endregion
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
    //
    public int GetTotalScore()
    {
        return totalScore;
    }
    //
    public void RestoreValues()
    {
        NivelJugado = new bool[5];
        ScoreLevel = new int[] { 0, 0, 0, 0, 0 };
        IdPlayer = 0;
        AliasPlayer = "";
        LevelSelected = 0;
        PrevLevel = 0;
        Level = 0;
        Score = 0;
        totalScore = 0;
    }
    public void GuardarData()
    {
        //preguntamos si el puntaje obtenido en el Nivel es mayor al que esta almacenado en EstadoJuego
        if(Score >= ScoreLevel[Level])
        {
            ScoreLevel[Level] = Score;
        }
        totalScore = ScoreLevel[1] + ScoreLevel[2] + ScoreLevel[3] + ScoreLevel[4];
        System.DateTime dateTime = System.DateTime.Now;
        string fecha = dateTime.ToString("yyyy-MM-dd");
        string query = "INSERT INTO Registro (Id_jugador,Fecha,Nivel,Puntaje,Tiempo) " +
            "VALUES("+IdPlayer+",'"+fecha+"',"+Level+","+Score+",'"+Time+"');";
        SQLiteAndroid sql = new SQLiteAndroid();
        sql.Conectar();
        sql.insert_function(query);
        if (ScoreLevel[Level] == MaxScorePerLevel[Level])
        {
            NivelJugado[Level] = true;
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

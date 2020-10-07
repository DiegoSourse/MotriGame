using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;
    // Start is called before the first frame update
    #region UserData
    private int idPlayer;
    private string aliasPlayer;
    #endregion
    #region LevelData
    private int level;
    private string time;
    private int score;
    #endregion
    private string totalTime;
    private int totalScore=0;
    //
    private void Awake()
    {
        Debug.Log("Nivel actual: " + level);
        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //Sirve para enviar mensaje entre escenas sin necesidad de acceder al objeto
        //NotificationCenter.DefaultCenter.AddObserver(this,"");
    }

    // Update is called once per frame
    void Update()
    {

    }
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
}

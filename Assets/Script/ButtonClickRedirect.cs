using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
public class ButtonClickRedirect : MonoBehaviour
{
    //public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public Button Btn { get { return GetComponent<Button>(); } }
    //public AudioClip clip;
    string textoBoton = "";
    int idBoton = 0;
    void Start()
    {
        //gameObject.AddComponent<AudioSource>();
        textoBoton = Btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
        idBoton = Convert.ToInt32(Btn.GetComponentInChildren<Text>().text);
        Btn.onClick.AddListener(Direccionar);
    }
    void Direccionar()
    {
        //Debug.Log(GameObject.FindGameObjectsWithTag("ItemJugador").Length);
        Debug.Log(textoBoton);
        SQLiteAndroid sql = new SQLiteAndroid();
        sql.Conectar();
        string condi = "Id_Jugador=" + idBoton;
        using (IDataReader reader = sql.Select_function("Jugador", "Nombre,Alias,Edad,Estatura", condi))
        {
            while (reader.Read())
            {
                EstadoJuego.estadoJuego.Nombre = reader.GetString(0);
                //EstadoJuego.estadoJuego.SetAlias
                EstadoJuego.estadoJuego.Edad = reader.GetInt32(2);
                EstadoJuego.estadoJuego.Estatura = reader.GetFloat(3);
            }
            //Debug.Log(reader.FieldCount);
        }
        EstadoJuego.estadoJuego.AliasPlayer = textoBoton;
        EstadoJuego.estadoJuego.IdPlayer = idBoton;
        //redireccionamos a la escena 01
        //con los datos de estado juego
        SceneManager.LoadScene("Nivel_01");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;
public class SQLiteAndroid// : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    public InputField t_name, t_Address, t_id;
    public Text data_staff;

    string DatabaseName = "motrigame.s3db";
    // Start is called before the first frame update
    public void Conectar()
    {
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        conn = "URI=file:" + filepath;
        //Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        CreateSchema();
    }
    void CreateSchema()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            using (var cmd = dbconn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Jugador"+
                                  "(Id_jugador           INTEGER NOT NULL PRIMARY KEY,"+
                                  " Alias                VARCHAR(100) NOT NULL UNIQUE,"+
                                  " Nombre               VARCHAR(200) NULL,"+
                                  " Edad                 INTEGER NULL,"+
                                  " Estatura             FLOAT NULL,"+
                                  " Sexo                 VARCHAR(1) NULL ); "+
                                  "CREATE TABLE IF NOT EXISTS Logro"+
                                  "(Id_logro INTEGER NOT NULL PRIMARY KEY," +
                                  " Nombre VARCHAR(200) NOT NULL," +
                                  " Descripcion          VARCHAR(500) NOT NULL," +
                                  " Puntaje              INTEGER NULL," +
                                  " Tiempo               TIME NULL," +
                                  " Nivel                INTEGER NOT NULL," +
                                  " Premio VARCHAR(200) NOT NULL );" +
                                  "CREATE TABLE IF NOT EXISTS Jugador_Logro" +
                                  "( Id_logro INTEGER NOT NULL," +
                                  " Id_jugador INTEGER NOT NULL," +
                                  " Alias VARCHAR(100) NOT NULL," +
                                  " Fecha                DATE NULL," +
                                  " PRIMARY KEY(Id_jugador, Id_logro)," +
                                  " FOREIGN KEY(Id_jugador) REFERENCES Jugador(Id_jugador)," +
                                  " FOREIGN KEY(Id_logro) REFERENCES Logro(Id_logro) );" +
                                  "CREATE TABLE IF NOT EXISTS Registro" +
                                  "( Id_reg INTEGER NOT NULL PRIMARY KEY," +
                                  " Id_jugador INTEGER NOT NULL," +
                                  " Fecha DATE NOT NULL," +
                                  " Nivel INTEGER NOT NULL," +
                                  " Puntaje INTEGER NOT NULL," +
                                  " Tiempo TIME NOT NULL," +
                                  " FOREIGN KEY (Id_jugador) REFERENCES Jugador (Id_jugador) );";
                var result = cmd.ExecuteNonQuery();
                //Debug.Log("create schema: " + result);
            }
        }
    }
    //Insert To Database
    public int insert_function(string query)
    {
        int idLast=0;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            using (dbcmd = dbconn.CreateCommand())
            {
                sqlQuery = string.Format(query);// table name
                dbcmd.CommandText = sqlQuery;
                idLast= dbcmd.ExecuteNonQuery();                
            }                
        }
        Debug.Log("Insert Done  ");
        return idLast;
    }
    //Read All Data For To Database
    public IDataReader Select_function(string table, string items, string condition="1")
    {
        //Debug.Log(dbconn.State);
        dbconn = new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT " + items + " FROM " + table + " WHERE " + condition;// table name        
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }
    public IDataReader Select_function_Query(string query)
    {
        Debug.Log(dbconn.State);
        dbconn = new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = query;// table name        
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }
    public void Estado()
    {
        Debug.Log("La variable dbconn:" + dbconn.State);
    }
    public void Cerrar()
    {
        dbconn.Close();
    }
    public int Select_Scalar_Int(string query)
    {
        int entero = 0;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            using (dbcmd = dbconn.CreateCommand())
            {
                sqlQuery = string.Format(query);// table name
                dbcmd.CommandText = sqlQuery;
                entero= Convert.ToInt32(dbcmd.ExecuteScalar());
            }
        }
        return entero;
    }
}
class jugador
{
    public int id;
    public string alias;
    public jugador(int id,string alias)
    {
        this.id = id;
        this.alias = alias;
    }
}
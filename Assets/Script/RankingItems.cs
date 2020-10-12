using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
public class RankingItems : MonoBehaviour
{
    public GameObject item;
    public GameObject container;
    public int option = 0;
    SQLiteAndroid sql;
    // Start is called before the first frame update
    private void Awake()
    {        
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnDisable()
    {
        Debug.Log("I'm disabled "+option);
    }
    private void OnEnable()
    {
        sql = new SQLiteAndroid();
        sql.Conectar();
        //colocamos un if que permita seleccionar que debe realizar el script
        switch (option)
        {
            case 1:
                //Ranking los 10 primero valores posicion, alias, puntaje total, tiempo total, fecha
                string query = "Select b.Alias, SUM(a.puntaje) as score, MIN(a.Tiempo), MAX(a.Fecha) from registro a, (Select Id_jugador,Alias from Jugador)b where a.Id_Jugador=b.Id_Jugador Group by a.Id_jugador order by score DESC;";
                using (IDataReader reader = sql.Select_function_Query(query))
                {
                    int pos = 1;
                    while (reader.Read())
                    {
                        //Debug.Log(reader.GetString(0));
                        GameObject card1 = Instantiate(item) as GameObject;
                        card1.transform.SetParent(container.transform, false);
                        TMPro.TextMeshProUGUI[] texto1 = card1.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                        //Button boton = card.GetComponent<Button>();
                        texto1[0].text = pos.ToString();
                        texto1[1].text = reader.GetString(0);
                        texto1[2].text = reader.GetInt32(1).ToString();
                        texto1[3].text = reader.GetString(2);
                        texto1[4].text = reader.GetString(3);
                        pos++;
                    }
                    //Debug.Log(reader.FieldCount);
                }
                break;
            case 2:
                //Progreso con id y alias de jugador Nivel, puntos por nivel, tiempo por nivel, fecha
                string condicion = "Id_Jugador="+EstadoJuego.estadoJuego.GetId()+";";
                using (IDataReader reader = sql.Select_function("Registro", "Nivel,Puntaje,Tiempo,Fecha",condicion))
                {
                    while (reader.Read())
                    {
                        //Debug.Log(reader.GetString(0));
                        GameObject card2 = Instantiate(item) as GameObject;
                        card2.transform.SetParent(container.transform, false);
                        TMPro.TextMeshProUGUI[] texto2 = card2.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
                        texto2[0].text = "Nivel "+reader.GetInt32(0).ToString();
                        texto2[1].text = reader.GetInt32(1).ToString();
                        texto2[2].text = reader.GetString(2);
                        texto2[3].text = reader.GetString(3);
                    }
                    //Debug.Log(reader.FieldCount);
                }
                break;
            case 3:
                //Logro con id y alias de jugador, nivel, descripcion y fecha
                break;
            case 4:
                //Usuarios mostrar todos los usuarios por sus alias y su id
                //query = "SELECT * FROM Jugador";
                using (IDataReader reader = sql.Select_function("Jugador", "Id_Jugador,Alias","1 Order by Id_Jugador"))
                {
                    while (reader.Read())
                    {
                        //Debug.Log(reader.GetString(0));
                        GameObject card = Instantiate(item) as GameObject;
                        card.transform.SetParent(container.transform, false);
                        TMPro.TextMeshProUGUI texto = card.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                        //Button boton = card.GetComponent<Button>();
                        texto.text = reader.GetString(1);
                        Text id = card.GetComponentInChildren<Text>();
                        id.text = reader.GetInt32(0).ToString();
                    }
                    //Debug.Log(reader.FieldCount);
                }
                break;
            default:
                break;
        }
        /*for (int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(item) as GameObject;
            card.transform.SetParent(container.transform, false);
        }*/
        sql.Cerrar();
    }
}

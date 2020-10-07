using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankingItems : MonoBehaviour
{
    public GameObject item;
    public GameObject container;
    public int option = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        //colocamos un if que permita seleccionar que debe realizar el script
        switch (option)
        {
            case 1:
                //Ranking los 10 primero valores posicion, alias, puntaje total, tiempo total, fecha
                break;
            case 2:
                //Progreso con id y alias de jugador Nivel, puntos por nivel, tiempo por nivel, fecha
                break;
            case 3:
                //Logro con id y alias de jugador, nivel, descripcion y fecha
                break;
            case 4:
                //Usuarios mostrar todos los usuarios por sus alias y su id
                break;
            default:
                break;
        }
        /*for (int i = 0; i < 10; i++)
        {
            GameObject card = Instantiate(item) as GameObject;
            card.transform.SetParent(container.transform, false);
        }*/
    }
    // Update is called once per frame
    void Update()
    {

    }
}

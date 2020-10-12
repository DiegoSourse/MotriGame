using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonAnalizer : MonoBehaviour
{
    private void OnEnable()
    {
        for (int i = 1; i <= 4; i++)
        {
            GameObject bo = GameObject.Find("BtNivel" + i);
            Button boton = bo.GetComponentInChildren<Button>();
            boton.interactable = !EstadoJuego.estadoJuego.NivelJugado[i];
            //Debug.Log(bo.name+"---"+ EstadoJuego.estadoJuego.NivelJugado[i
            //Podemos cambiar el color o colocar un aviso de que se cumplio el nivel
        }
    }
}

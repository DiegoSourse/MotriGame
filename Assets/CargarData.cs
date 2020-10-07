using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CargarData : MonoBehaviour
{
    private string alias;
    void Awake()
    {
        alias=EstadoJuego.estadoJuego.GetAlias();
        //
    }
    void Start()
    {
        GameObject go = GameObject.Find("NombreJugador");
        TextMeshProUGUI newAlias = go.GetComponent<TextMeshProUGUI>();
        newAlias.text="Hola "+alias+"!";
    }
}

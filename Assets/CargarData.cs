using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CargarData : MonoBehaviour
{
    private string alias;
    private int prevLevel;
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
    public void CargarPerfil(GameObject obj)
    {
        TextMeshProUGUI[] lb = obj.GetComponentsInChildren<TextMeshProUGUI>();
        lb[0].text = "Nombre: " + EstadoJuego.estadoJuego.Nombre;
        lb[1].text = "Alias: " + EstadoJuego.estadoJuego.GetAlias();
        lb[2].text = "Edad: " + EstadoJuego.estadoJuego.Edad.ToString() + " años";
        lb[3].text = "Estatura: " + EstadoJuego.estadoJuego.Estatura + " m";
        //Debug.Log(EstadoJuego.estadoJuego.Estatura);
    }
}

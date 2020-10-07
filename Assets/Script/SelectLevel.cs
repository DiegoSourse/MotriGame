using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    //private int nivel=0;
    public TextMeshProUGUI tituloNivel;
    public TextMeshProUGUI nombreNivel;
    //private int level = 0;
    public void DireccionaNivel(int nivel)
    {
        string titulo = "";
        string subtitulo = "";
        switch (nivel)
        {
            case 1:
                titulo = "Nivel 1";
                subtitulo = "Marca a la abeja";
                break;
            case 2:
                titulo = "Nivel 2";
                subtitulo = "Evita los troncos";
                break;
            case 3:
                titulo = "Nivel 3";
                subtitulo = "Cocodrilos y Estrellas";
                break;
            case 4:
                titulo = "Nivel 4";
                subtitulo = "Salmones y Estrellas";
                break;
            default:
                break;
        }
        tituloNivel.text = titulo;
        nombreNivel.text = subtitulo;
        //level = nivel;
        EstadoJuego.estadoJuego.Setlevel(nivel);
        Debug.Log(nivel);
    }
    private void Start()
    {
        //EstadoJuego.estadoJuego.Setlevel(level);
        //Debug.Log(EstadoJuego.estadoJuego.Getlevel());
    }
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController5 : MonoBehaviour
{
    public GameObject star;
    public TMPro.TextMeshProUGUI texto;
    public TMPro.TextMeshProUGUI textoScore;
    private MeshRenderer estrella;
    private int puntaje;
    private string Nombre;
    private string HexColor;
    // Start is called before the first frame update
    private Color[] colores = {
        new Color(1f,0f,0f,1f),
        new Color(0f,1f,0f,1f),
        new Color(0f,0f,1f,1f),
        new Color(0.839216f,0.537255f,0.062745f,1f),
        new Color(0.160784f,0.501961f,0.725490f,1f),
        new Color(0.129412f,0.184314f,0.239216f,1f)
    };
    private string[] NombreColor = {
        "Roja",
        "Verde",
        "Azul",
        "Amarilla",
        "Celeste",
        "Gris"
    };
    private void Awake()
    {
    }
    void Start()
    {
        puntaje = EstadoJuego.estadoJuego.GetTotalScore();
        estrella = star.GetComponentInChildren<MeshRenderer>();
        estrella.material.color = Verifica(puntaje);
        texto.text = "Ganaste la estrella <#"+HexColor+">" + Nombre+"</color>";
        textoScore.text = puntaje.ToString();
    }
    private void Update()
    {
        estrella.material.color = Verifica(puntaje);
        texto.text = "Ganaste la estrella <#" + HexColor + ">" + Nombre + "</color>";
    }
    // Update is called once per frame
    ///
    public void SalirNivel()
    {
        SceneManager.LoadScene("Nivel_01");
    }
    Color Verifica(int nro)
    {
        if (nro < 90)
        {
            if(nro < 80)
            {
                if(nro < 70)
                {
                    if(nro < 60)
                    {
                        if(nro < 50)
                        {
                            Nombre = NombreColor[5];
                            HexColor = ColorUtility.ToHtmlStringRGB(colores[5]);
                            return colores[5];
                        }
                        Nombre = NombreColor[4];
                        HexColor = ColorUtility.ToHtmlStringRGB(colores[4]);
                        return colores[4];
                    }
                    Nombre = NombreColor[3];
                    //HexColor = ColorUtility.ToHtmlStringRGB(colores[3]);
                    HexColor = "f7dc6f";
                    return colores[3];
                }
                Nombre = NombreColor[2];
                HexColor = ColorUtility.ToHtmlStringRGB(colores[2]);
                return colores[2];
            }
            Nombre = NombreColor[1];
            HexColor = ColorUtility.ToHtmlStringRGB(colores[1]);
            return colores[1];
        }
        Nombre = NombreColor[0];
        HexColor = ColorUtility.ToHtmlStringRGB(colores[0]);
        return colores[0];
    }
}

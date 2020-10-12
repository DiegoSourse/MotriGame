using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ApplicationManager : MonoBehaviour
{
    public void CambioEscena(string escena)
    {
        if(escena == "Nivel_00") //si volvemos al inicio
        {
            EstadoJuego.estadoJuego.RestoreValues();
        }else if(escena == "Nivel_01") //cuando se ha jugado un nivel en especifico y volvemos al menu de jugador
        {
            //EstadoJuego.estadoJuego.NivelJugado[EstadoJuego.estadoJuego.Getlevel()] = true;
            EstadoJuego.estadoJuego.GuardarData();
        }
        SceneManager.LoadScene(escena);
    }
    public void CambioEscena()
    {
        int nivel = EstadoJuego.estadoJuego.LevelSelected;
        EstadoJuego.estadoJuego.NivelJugado[EstadoJuego.estadoJuego.GetPrevLevel()] = true;
        EstadoJuego.estadoJuego.SetPrevLevel(EstadoJuego.estadoJuego.Getlevel());
        EstadoJuego.estadoJuego.Setlevel(nivel);
        string escena = "Nivel_" + nivel;
        EstadoJuego.estadoJuego.LevelSelected = 0;
        /*Debug.Log("Temp: " + EstadoJuego.estadoJuego.LevelSelected);
        Debug.Log("Prev_Level: " + EstadoJuego.estadoJuego.GetPrevLevel());
        Debug.Log("Level: " + EstadoJuego.estadoJuego.Getlevel());
        //Debug.Log("NIVELES: " + EstadoJuego.estadoJuego.NivelJugado);
        foreach (bool i in EstadoJuego.estadoJuego.NivelJugado)
            print(i);*/
        SceneManager.LoadScene(escena);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}

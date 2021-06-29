using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//
using UnityEngine.XR;
public class ApplicationManager : MonoBehaviour
{
    public GameObject Canva;
    public GameObject previo;
    private Slider slider;
    private TMPro.TextMeshProUGUI texto;
    public void CambioEscena(string escena)
    {
        if (escena == "Nivel_00") //si volvemos al inicio
        {
            EstadoJuego.estadoJuego.RestoreValues();
        } /*else if (escena == "Nivel_01") //cuando se ha jugado un nivel en especifico y volvemos al menu de jugador
        {
            //EstadoJuego.estadoJuego.NivelJugado[EstadoJuego.estadoJuego.Getlevel()] = true;
            EstadoJuego.estadoJuego.GuardarData();
        }*/
        SceneManager.LoadScene(escena);
    }
    public void CambioEscena()
    {
        int nivel = EstadoJuego.estadoJuego.LevelSelected;
        EstadoJuego.estadoJuego.NivelJugado[EstadoJuego.estadoJuego.PrevLevel] = true;
        EstadoJuego.estadoJuego.PrevLevel = EstadoJuego.estadoJuego.Level;
        EstadoJuego.estadoJuego.Level = nivel;
        string escena = "Nivel_" + nivel;
        EstadoJuego.estadoJuego.LevelSelected = 0;
        //
        slider = Canva.GetComponentInChildren<Slider>();
        texto = slider.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        //previo.SetActive(false);
        StartCoroutine(LoadAsynchronously(escena, slider, texto));
        //        
        /*Debug.Log("Temp: " + EstadoJuego.estadoJuego.LevelSelected);
        Debug.Log("Prev_Level: " + EstadoJuego.estadoJuego.GetPrevLevel());
        Debug.Log("Level: " + EstadoJuego.estadoJuego.Getlevel());
        //Debug.Log("NIVELES: " + EstadoJuego.estadoJuego.NivelJugado);
        foreach (bool i in EstadoJuego.estadoJuego.NivelJugado)
            print(i);*/
        //SceneManager.LoadScene(escena);
    }
    IEnumerator LoadAsynchronously(string escena, Slider s, TMPro.TextMeshProUGUI t)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(escena);
        previo.SetActive(false);
        Canva.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            s.value = progress;
            t.text = (progress * 100f).ToString("0.0") + "%";
            yield return null;
        }
    }
    public void Quit()
    {
        //
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}

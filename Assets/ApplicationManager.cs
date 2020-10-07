using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ApplicationManager : MonoBehaviour
{
    public void CambioEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }
    public void CambioEscena()
    {
        int nivel=EstadoJuego.estadoJuego.Getlevel();
        string escena = "Nivel_" + nivel;
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

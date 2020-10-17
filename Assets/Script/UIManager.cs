using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;
public class UIManager : MonoBehaviour
{

    public Animator initiallyOpen;

    private int m_OpenParameterId;
    private Animator m_Open;
    private GameObject m_PreviouslySelected;

    const string k_OpenTransitionName = "Open";
    const string k_ClosedStateName = "Closed";

    public void OnEnable()
    {
        m_OpenParameterId = Animator.StringToHash(k_OpenTransitionName);

        if (initiallyOpen == null)
            return;

        OpenPanel(initiallyOpen);
    }

    public void OpenPanel(Animator anim)
    {
        if (m_Open == anim)
            return;

        anim.gameObject.SetActive(true);
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        m_Open = anim;
        m_Open.SetBool(m_OpenParameterId, true);

        GameObject go = FindFirstEnabledSelectable(anim.gameObject);

        SetSelected(go);
    }

    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive() && selectable.IsInteractable())
            {
                go = selectable.gameObject;
                break;
            }
        }
        return go;
    }

    public void CloseCurrent()
    {
        if (m_Open == null)
            return;

        m_Open.SetBool(m_OpenParameterId, false);
        SetSelected(m_PreviouslySelected);
        StartCoroutine(DisablePanelDeleyed(m_Open));
        m_Open = null;
    }

    IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
            anim.gameObject.SetActive(false);
    }

    private void SetSelected(GameObject go)
    {
        EventSystem.current.SetSelectedGameObject(go);
    }
    public void AbrirEnalce()
    {
        Application.OpenURL("https://diegoquispecondori.blogspot.com/");
    }
    public void ObtenerDatosForm(GameObject form)
    {
        if (VerificaForm(form))
        {
            GameObject nombre = GameObject.Find("nombre");
            GameObject alias = GameObject.Find("alias");
            GameObject edad = GameObject.Find("edad");
            GameObject estatura = GameObject.Find("estatura");
            GameObject masc = GameObject.Find("masc");
            GameObject fem = GameObject.Find("fem");
            string newNombre = nombre.GetComponent<TMPro.TMP_InputField>().text;
            string newAlias = alias.GetComponent<TMPro.TMP_InputField>().text;
            string newEdad = edad.GetComponent<TMPro.TMP_InputField>().text;
            string newEstatura = estatura.GetComponent<TMPro.TMP_InputField>().text;
            Toggle newMasc = masc.GetComponent<Toggle>();
            Toggle newFem = fem.GetComponent<Toggle>();
            //
            string sexo = newMasc.isOn ? "M" : "F";
            string aliasDef = newAlias.ToUpper();
            //conectamos a la base de datos
            SQLiteAndroid sql = new SQLiteAndroid();
            sql.Conectar();
            //verificamos si el alias es unico, caso contrario añadimos un numero adicinal
            int dato = sql.Select_Scalar_Int("SELECT count(Alias) from Jugador where Alias like '" + aliasDef + "%';");
            aliasDef = aliasDef + (dato != 0 ? "" + dato : "");
            //ingresamos los datos obtenidos a la base de datos
            string query = "INSERT INTO Jugador (Alias,Nombre,Edad,Estatura,Sexo) VALUES('" + aliasDef + "','" + newNombre + "'," + newEdad + "," + newEstatura + ",'" + sexo + "')";
            sql.insert_function(query);
            //Debug.Log(query);
            int Id_UsuarioNuevo = sql.Select_Scalar_Int("Select Id_jugador from jugador where alias='" + aliasDef + "' limit 1;");
            sql.Cerrar();
            //Establecemos como id y alias general para las proximas escenas
            EstadoJuego.estadoJuego.AliasPlayer = aliasDef;
            EstadoJuego.estadoJuego.IdPlayer = Id_UsuarioNuevo;
            EstadoJuego.estadoJuego.Nombre = newNombre;
            EstadoJuego.estadoJuego.Estatura = Convert.ToSingle(newEstatura, CultureInfo.CreateSpecificCulture("en-US"));
            EstadoJuego.estadoJuego.Edad = Convert.ToInt32(newEdad);
            //Debug.Log(EstadoJuego.estadoJuego.GetAlias());
            //Cargamos a la siguiente escena
            SceneManager.LoadScene("Nivel_01");
            //una corutina para la barra de progreso
        }
        else
        {

        }
        
    }
    public void Abre_user(Button click)
    {
        string alias = click.GetComponent<TMPro.TextMeshProUGUI>().text;
        Debug.Log("Del boton clickeado "+alias);
    }

    public bool VerificaForm(GameObject form)
    {
        Color newcolor = new Color(0.898f, 0.341f,  0.341f, 1f);
        bool llenado = true;
        TMPro.TMP_InputField[] lista = form.GetComponentsInChildren<TMPro.TMP_InputField>();
        foreach (var item in lista)
        {
            if (item.text == "")
            {
                Debug.Log("Error, campos vacios en el formulario " + item.name);
                //item.
                item.image.color = newcolor;
                llenado = false;
                //return false;
                //break;
            }
        }
        return llenado;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormFunctions : MonoBehaviour
{
    public void LimpiaFormulario(GameObject form)
    {
        TMPro.TMP_InputField[] lista = form.GetComponentsInChildren<TMPro.TMP_InputField>();
        foreach (var item in lista)
        {
            item.text = "";
        }
    }
    public void cambiaColor(TMPro.TMP_InputField input)
    {
        Color newcolor = new Color(1f, 1f, 1f, 1f);
        //TMPro.TMP_InputField lista = gameObject.GetComponentInChildren<TMPro.TMP_InputField>();
        input.image.color = newcolor.linear;
        //Debug.Log(this.gameObject.name + " was selected");
    }
}

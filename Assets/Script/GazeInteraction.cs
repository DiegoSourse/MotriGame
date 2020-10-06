using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Collider))]
public class GazeInteraction : MonoBehaviour, IPointerClickHandler
{
    public float gazeTime = 2f;
    private int score = 0;
    //public Text texto;
    private float timer;
    private bool gazedAt;

    public TextMeshPro TMscore;

    // Use this for initialization
    void Start()
    {
        TMscore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt)
        {
            timer += Time.deltaTime;
            //Debug.Log("Time Gaze: "+timer);
            if (timer >= gazeTime)
            {
                // execute pointerdown handler
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                timer = 0f;
            }
        }else timer = 0f;

    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Salida para consolar el nombre del GameObject cliqueado y el siguiente mensaje. Puede reemplazar esto con sus propias acciones para hacer clic en GameObject .
        Debug.Log(gameObject.name + "¡Juego en el que se hizo clic!");
        score += 1;
        TMscore.text = score.ToString();
        if (score == 5)
            Time.timeScale = 0f;
    }
    public void PointerEnter()
    {
        gazedAt = true;
        Debug.Log("PointerEnter");
    }

    public void PointerExit()
    {
        gazedAt = false;
        Debug.Log("PointerExit");
    }

    public void PointerDown()
    {
        Debug.Log("PointerDown");
    }
}

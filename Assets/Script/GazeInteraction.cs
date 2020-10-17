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
    //public Text texto;
    private float timer=0f;
    //public float Radial=0f;
    private bool gazedAt;
    private bool aux;
    //public Transform RadialProgress;
    public GameObject radio;
    //
    public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;
    //
    //public TextMeshPro TMscore;

    // Use this for initialization
    void Start()
    {
        //TMscore.text = "0";
        radio.GetComponent<Image>().fillAmount = timer;
        //RadialProgress.GetComponent<Image>().fillAmount=timer;
        gameObject.AddComponent<AudioSource>();
        aux = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (gazedAt && aux==false)
        {
            timer += Time.deltaTime;
            //Debug.Log("Time Gaze: "+timer);
            //RadialProgress.GetComponent<Image>().fillAmount = timer / gazeTime;
            radio.GetComponent<Image>().fillAmount = timer / gazeTime;

            //Radial = RadialProgress.GetComponent<Image>().fillAmount;
            //Radial = radio.GetComponent<Image>().fillAmount;

            //Debug.Log(Radial),
            if (timer >= gazeTime)
            {
                // execute pointerdown handler
                ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                timer = 0f;
                aux = true;
                //sonido de clic
                
            }
        }
        else
        {
            timer = 0f;
            //RadialProgress.GetComponent<Image>().fillAmount = timer;
            //radio.GetComponent<Image>().fillAmount = 0f;
        }

    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Salida para consolar el nombre del GameObject cliqueado y el siguiente mensaje. Puede reemplazar esto con sus propias acciones para hacer clic en GameObject .
        Debug.Log(gameObject.name + "¡Juego en el que se hizo clic!");
        source.PlayOneShot(clip);
        radio.GetComponent<Image>().fillAmount = 0f;
        //enviamos un datos de que se ha hecho clic en el objeto para incrementar el score
        /*score += 1;
        TMscore.text = score.ToString();
        if (score == 5)
            Time.timeScale = 0f;*/

    }
    public void PointerEnter()
    {
        gazedAt = true;
        Debug.Log("PointerEnter");
    }

    public void PointerExit()
    {
        gazedAt = false;
        aux = false;
        if(radio != null)
            radio.GetComponent<Image>().fillAmount = 0f;
        Debug.Log("PointerExit");
    }

    public void PointerDown()
    {
        Debug.Log("PointerDown");
    }

}

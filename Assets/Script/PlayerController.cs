using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Alerta;
    private AudioSource PSourse;
    public AudioClip Colision;
    // Start is called before the first frame update
    void Start()
    {
        Alerta.SetActive(false);
        PSourse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "proyectil")
        {
            Alerta.SetActive(true);
            //Iniciamos un sonido de choque suave
            PSourse.PlayOneShot(Colision);
            //Mandamos la señal de choque al observador
            NotificationCenter.DefaultCenter.PostNotification(this,"TriggerPlayer");
        }
        if (other.tag == "salmon")
        {
            Alerta.SetActive(true);
            //Iniciamos un sonido de choque suave
            PSourse.PlayOneShot(Colision);
            //Mandamos la señal de choque al observador
            NotificationCenter.DefaultCenter.PostNotification(this, "TriggerSalmon");
        }
        if(other.tag == "cocodrilo")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "TriggerCroco");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "proyectil" ||other.tag == "salmon")
        {
            Alerta.SetActive(false);
        }
        if(other.tag == "cocodrilo")
        {
            NotificationCenter.DefaultCenter.PostNotification(this, "ExitCroco");
        }
    }
}

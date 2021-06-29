using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalmonController : MonoBehaviour
{
    public AudioClip Zambullirse;
    public AudioClip SalirAgua;
    private AudioSource Source;
    private Animator Salmon;
    private int cont;
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        Salmon = GetComponentInChildren<Animator>();
        cont = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "agua")
        {
            cont++;
            //mandamos una señal al observer de que se realizo colision con Player
            //Debug.Log("SPLASHHHH Enter");
            if (cont < 2)
            {
                Source.PlayOneShot(SalirAgua);
                Salmon.SetBool("saltar", true);
            }
            else
            {
                cont = 0;
                Source.PlayOneShot(Zambullirse);
                Salmon.SetBool("saltar", false);
                NotificationCenter.DefaultCenter.PostNotification(this, "SalmonSplash");
                //Debug.Log("Splash");
            }
        }
    }
}

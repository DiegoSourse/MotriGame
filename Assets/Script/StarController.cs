using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    /*public AudioSource source { get { return GetComponent<AudioSource>(); } }
    public Button btn { get { return GetComponent<Button>(); } }
    public AudioClip clip;*/
    private AudioSource ASourse;
    public AudioClip Spawn;
    public AudioClip No;
    private bool ChoqueSalmon;
    private bool ChoqueTronco;
    // Start is called before the first frame update
    void Start()
    {
        ASourse = GetComponent<AudioSource>();
        ChoqueSalmon = false;
        ChoqueTronco = false;
    }

    // Update is called once per frame
    void Update()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, "TriggerSalmon");
        NotificationCenter.DefaultCenter.AddObserver(this, "TriggerPlayer");
    }
    void TriggerSalmon()
    {
        ChoqueSalmon = true;
    }
    void TriggerPlayer()
    {
        ChoqueTronco = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Reproducimos el sonido de estrella
            if(ChoqueSalmon == true || ChoqueTronco == true)
            {
                //reproducimos error
                ASourse.PlayOneShot(No);
                ChoqueSalmon = false;
                ChoqueTronco = false;
            }else
                ASourse.PlayOneShot(Spawn);
            //mandamos una señal al observer de que se realizo colision con Player
            NotificationCenter.DefaultCenter.PostNotification(this, "TriggerStar");
        }
    }
}

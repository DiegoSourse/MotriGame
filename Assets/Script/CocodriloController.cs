using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocodriloController : MonoBehaviour
{
    private Animator Anime;
    //
    public AudioClip Alerta;
    public AudioClip Morder;
    public AudioClip Girar;
    private AudioSource Source;
    //
    private Collider vision;
    //
    // Start is called before the first frame update
    private void Awake()
    {
        Anime = GetComponentInChildren<Animator>();
        Source = GetComponent<AudioSource>();
        vision = transform.GetChild(1).gameObject.GetComponent<Collider>();
    }
    void Start()
    {
        CambiaClip(Alerta);
    }

    // Update is called once per frame
    void Update()
    {
        //Existio colision con la estrella??
        NotificationCenter.DefaultCenter.AddObserver(this, "TriggerStar");
        //Player entra en el campo de vison del cocodrilo?
        NotificationCenter.DefaultCenter.AddObserver(this, "TriggerCroco");
        //Player sale del campo de vision del cocodrilo?
        NotificationCenter.DefaultCenter.AddObserver(this, "ExitCroco");
    }
    void TriggerStar()
    {
        Debug.Log("Choque con estrella");
        //cocodrilo empieza a girar
        Anime.SetBool("colision", true);
        //Anime.SetTrigger("colisionStar");
        CambiaClip(Girar);
    }
    void TriggerCroco()
    {
        //Anime.SetTrigger("colisionStar");
        Anime.SetBool("colision", false);
        Debug.Log("Visto por cocodrilo");
        //cocodrilo empieza a morder
        Anime.SetBool("visto", true);
        CambiaClip(Morder);
    }
    void ExitCroco()
    {
        Debug.Log("Dejo de ser visto por cocodrilo");
        //cocodrilo vuelve al estado alerta
        Anime.SetBool("visto", false);
        CambiaClip(Alerta);
    }
    void CambiaClip(AudioClip New)
    {
        Source.clip = New;
        Source.Play();
    }
}

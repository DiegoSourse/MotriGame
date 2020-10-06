using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;
    // Start is called before the first frame update
    private void Awake()
    {
        if (estadoJuego==null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego!=this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {   
        //Sirve para enviar mensaje entre escenas sin necesidad de acceder al objeto
        //NotificationCenter.DefaultCenter.AddObserver(this,"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

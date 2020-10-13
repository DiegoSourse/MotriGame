using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayAnimAbeja : MonoBehaviour
{
    //public Animation anim;
    public Animator animCon;
    private Vector3 startingPosition;
    private Vector3 angulos;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick()
    {
        Debug.Log("Abeja Marcada");
        animCon.SetBool("Marcado", true);
        animCon.SetBool("Movimiento", true);
        NotificationCenter.DefaultCenter.PostNotification(this, "AbejaColision");
        NotificationCenter.DefaultCenter.PostNotification(this, "Colision");
        //cambiar de posicion de inmediato
    }
    //AÑADIMOS FUNCIONES PARA ONENTER, ONEXIT, ONCLICK
    public void Apuntar(bool valor)
    {
        animCon.SetBool("Apuntado", valor);
    }
}

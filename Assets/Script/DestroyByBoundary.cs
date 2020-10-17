using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    private float time=0;
    private bool trigger = false;
    private void OnTriggerEnter(Collider other)
    {
        //time += Time.deltaTime;
        trigger = true;
        //Debug.Log("Inicio");
    }
    private void OnTriggerStay(Collider other)
    {
        //if(other.tag=="proyectil")
            //time += Time.deltaTime;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "proyectil")
        {
            Destroy(other.gameObject);
            trigger = false;
            //Debug.Log("ELIMINADO");
            //Añadir una Notification
            NotificationCenter.DefaultCenter.PostNotification(this,"TriggerExit");
        }
    }
    private void Update()
    {
        if (trigger)
        {
            time += Time.deltaTime;
            //Debug.Log("Final " + time);
        }
    }
}

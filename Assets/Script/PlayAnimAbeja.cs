using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayAnimAbeja : MonoBehaviour
{
    public Animation anim;
    private Vector3 startingPosition;
    private Vector3 angulos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        /*startingPosition = transform.localPosition;
        angulos = transform.localEulerAngles;
        Debug.Log("INICIO: "+ startingPosition.ToString());
        Debug.Log("INICIO ANGULO: " + angulos.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void Reset()
    {
        GameObject MoverAbeja = gameObject;
        MoverAbeja.transform.localPosition = startingPosition;
        //MoverAbeja.transform.localRotation = angulos;
    }*/
    public void OnPointerClick()
    {
        Debug.Log("Abeja Marcada");
        NotificationCenter.DefaultCenter.PostNotification(this, "AbejaColision");
        //cambiar de posicion de inmediato
    }
    public void animTranquilo()
    {
        anim.Play("Armature|Vuelo_Tranquilo");
    }
    public void animVuelo()
    {
        anim.Play("Armature|Vuelo_Escape");
    }
    public void animApuntado()
    {
        anim.Play("Armature|Vuelo_Apuntado");
    }
    /*public void MoverAbeja()
    {
        Debug.Log(gameObject.name);
        GameObject MoverAbeja = gameObject;
        // Move to random new location ±90˚ horzontal.
        int angle = Random.Range(-180, 180);
        Debug.Log("ANGULO: " + angle.ToString());
        //
        Vector3 direction = Quaternion.Euler(0,angle, 0) * Vector3.forward;
        Debug.Log("DIRECCION: "+direction.ToString());
       // New location between 1.5m and 3.5m.
       float distance = (3.5f * Random.value) + 1.5f;
        Debug.Log("DISTANCIA: " + distance.ToString());
        Vector3 newPos = direction * distance;
        Debug.Log("NEW POS: " + newPos.ToString());
        // Limit vertical position to be fully in the room.
        newPos.y = Mathf.Clamp(newPos.y, 1.9f, 4f);
        Debug.Log("NEW POS FINAL: " + newPos.ToString());
        MoverAbeja.transform.rotation = Quaternion.Euler(0,180+angle,0);
        MoverAbeja.transform.localPosition = newPos;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    //public Animation anim;
    public Animator animCon;
    public float initialtimer = 2f;
    private float decrement = 0;

    public float movementSpeed = 1f;
    public float rotationSpeed = 5f;
    public GameObject player;
    
    Vector3 targetPosition;
    Vector3 towardsTarget;
    private float porcentaje;
    float wanderRadius = 4.0f;
    private float timer;
    private float estatura, MaxH, radio;
    // Start is called before the first frame update
    void Start()
    {
        int level = EstadoJuego.estadoJuego.Level;
        estatura = EstadoJuego.estadoJuego.Estatura;
        //estatura = 1.1f;
        porcentaje = (estatura == 0 ? 1.8f : estatura * 1) / 1.8f;
        radio = wanderRadius * porcentaje;
        MaxH = radio - 0.5f;
        decrement = (initialtimer - 2f)/EstadoJuego.estadoJuego.MaxScorePerLevel[level];

        RecalculateTargetPosition();
        StartCoroutine(Volar());
    }
    void RecalculateTargetPosition()
    {
        do
        {
            targetPosition = player.transform.position + Random.onUnitSphere * radio;
        } while (targetPosition.y < 0.5f);          
        if (targetPosition.y > MaxH)
        {
            targetPosition.y = MaxH;
            targetPosition.z = MaxH;
        }
        //Debug.Log(targetPosition.ToString());
    }
    public void Colision()
    {
        Debug.Log("Marcado");
        initialtimer = initialtimer - decrement;
    }
    // Update is called once per frame
    /*void Update()
    {
        towardsTarget = targetPosition - transform.position;
        if (towardsTarget.magnitude < 0.25f)
        {
            Debug.Log("llego------");
            RecalculateTargetPosition();
        }
            
        Quaternion towardsTragetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, towardsTragetRotation, rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        Debug.DrawLine(transform.position, targetPosition, Color.red);
    }*/
    IEnumerator Volar()
    {
        while(true)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, "Colision");
            towardsTarget = targetPosition - transform.position;
            if (towardsTarget.magnitude < 0.25f)
            {
                // Debug.Log("llego------");
                //anim.Play("Armature|Vuelo_Tranquilo");
                
                animCon.SetBool("Movimiento", false);
                animCon.SetBool("Tiempo", true);
                animCon.SetBool("Marcado", false);
                //Espera hasta cambiar de posicion
                yield return new WaitForSeconds(initialtimer);                
                animCon.SetBool("Movimiento", true);
                animCon.SetBool("Tiempo", false);
                //anim.Stop("Armature|Vuelo_Tranquilo");
                //anim.Play("Armature|Vuelo_Escape");
                RecalculateTargetPosition();
            }
            Quaternion towardsTragetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, towardsTragetRotation, rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            //Debug.DrawLine(transform.position, targetPosition, Color.red);
            yield return null;
        }
    }
}

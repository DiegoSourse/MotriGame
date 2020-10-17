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

    float wanderRadius = 4.0f;
    private float timer;
    void RecalculateTargetPosition() {
        do
        {
            targetPosition = player.transform.position + Random.onUnitSphere * wanderRadius;

        } while (targetPosition.y < 0.5f);
                /*if (targetPosition.y < 1.9f)
            targetPosition.y = 1.9f;*/
        //Debug.Log(targetPosition.ToString());
    }
    // Start is called before the first frame update
    void Start()
    {
        int level = EstadoJuego.estadoJuego.Level;
        decrement = (initialtimer - 2f)/EstadoJuego.estadoJuego.MaxScorePerLevel[level];
        RecalculateTargetPosition();
        StartCoroutine(Volar());
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

using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class SaltoSalmonController : MonoBehaviour
{
    //[Tooltip("Position we want to hit")]
    public Vector3 targetPos { get; set; }
    public float speed { get; set; }
    public float arcHeight { get; set; }
    //
    public bool salto { get; set; }
    //
    public Vector3 startPos { get; set; }
    public char Eje { get; set; }
    //
    private bool llegada = false;
    void Start()
    {
        transform.position = startPos;
        salto = false;
        StartCoroutine("Esperar");
    }
    void Update()
    {
        if (salto == true)
        {
            char opcion = Eje;
            // Compute the next position, with arc added in
            float x0, x1, dist, nextX, baseY, arc;
            Vector3 nextPos = new Vector3();
            switch (opcion)
            {
                case 'x':       //Para Desplazarse en el eje X
                    x0 = startPos.x;
                    x1 = targetPos.x;
                    dist = x1 - x0;
                    nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
                    baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
                    arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                    nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
                    //Debug.Log("DIST: " + dist + "  NextX: " + nextX + "  BaseY: " + baseY + "  Arc: " + arc);
                    transform.LookAt(nextPos);
                    break;
                case 'z':       //Para Desplazarse en el eje Z
                    x0 = startPos.z;
                    x1 = targetPos.z;
                    //Debug.Log("XO: " + x0 + "   X1:" + x1);
                    dist = x1 - x0;
                    nextX = Mathf.MoveTowards(transform.position.z, x1, speed * Time.deltaTime);
                    baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
                    arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
                    //Debug.Log("DIST: " + dist + "  NextX: " + nextX + "  BaseY: " + baseY + "  Arc: " + arc);
                    nextPos = new Vector3(transform.position.x, baseY + arc, nextX);
                    transform.LookAt(nextPos);
                    break;
                default:
                    break;
            }
            transform.position = nextPos;
            // Do something when we reach the target
            if (nextPos == targetPos) Arrived();
        }
    }
    void Arrived()
    {
        salto = false;
        llegada = true;
        //Debug.Log("Llegada");
    }
    IEnumerator Esperar()
    {
        while (true)
        {
            if(llegada == true)
            {
                llegada = false;
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

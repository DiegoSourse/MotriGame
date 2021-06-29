using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class SaltoEntController : MonoBehaviour
{
    public Vector3 TargetPos { get; set; }
    public float Speed { get; set; }
    public float ArcHeight { get; set; }
    public bool Salto { get; set; }

    Vector3 startPos;

    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
        Salto = false;
    }

    void Update()
    {
        if (Salto == true)
        {
            // Compute the next position, with arc added in
            float x0 = startPos.x;
            float x1 = TargetPos.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, Speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, TargetPos.y, (nextX - x0) / dist);
            float arc = ArcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);
            // Rotate to face the next position, and then move there
            //transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;
            // Do something when we reach the target
            if (nextPos == TargetPos) Arrived();
        }
    }
    void Arrived()
    {
        //Destroy(gameObject);
        Salto = false;
        startPos = transform.position;
    }
    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
}

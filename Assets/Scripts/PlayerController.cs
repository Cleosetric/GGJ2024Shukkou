using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float rotationSpeed = 5f;
    public float upForce = 10f;
    public float raycastDistance = 1f;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    void Update()
    {
        RotateCylinder();

        
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, raycastDistance);
            Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.white);
            if (hit.collider != null)
            { 
                // Debug.Log(hit.collider.name + " hit by raycast");
                Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.red);
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    ApplyUpForce();
                }
        } 
    }

    void RotateCylinder()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
    }

    void ApplyUpForce()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * upForce, ForceMode2D.Impulse);
    }
}

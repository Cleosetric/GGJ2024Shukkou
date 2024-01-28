using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float upForce = 10f;
    public Collider2D hit;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-90 + angle, Vector3.forward);

        if (hit.isTrigger)
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                ApplyUpForce();
            }
        }
    }
    void ApplyUpForce()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * upForce, ForceMode2D.Impulse);
    }
}

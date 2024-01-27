using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public float boostForce = 10f;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            ApplyBoost(other.GetComponent<Rigidbody2D>());
        }
    }

    protected virtual void ApplyBoost(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Triggered with player contact");
    }
}

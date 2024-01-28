using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            ApplyBoost(other.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    protected virtual void ApplyBoost(Rigidbody2D rb)
    {
        rb.velocity = Vector2.zero;
        Debug.Log("Triggered with player contact");
    }
}

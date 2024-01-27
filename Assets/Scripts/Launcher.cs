using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Boost
{
    public Vector3 launchDirection = new Vector3(1, 1, 0);
    public float launchForce = 15f;

    protected override void ApplyBoost(Rigidbody2D rb)
    {
        base.ApplyBoost(rb); // Call the base ApplyBoost method to apply the boost force

        // Apply launch force in a specific direction
        rb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);
    }
}

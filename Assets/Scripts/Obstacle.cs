using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Boost
{
    public float linearDamping = 0.1f;
    public float angularDamping = 0.1f;

    protected override void ApplyBoost(Rigidbody2D rb)
    {
        rb.velocity *= (1 - linearDamping);
        rb.angularVelocity *= (1 - angularDamping);
        AudioManager.instance.Play("Crash");
    }
}

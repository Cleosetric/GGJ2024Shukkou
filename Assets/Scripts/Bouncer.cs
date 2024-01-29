using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Boost
{
    protected override void ApplyBoost(Rigidbody2D rb)
    {
        base.ApplyBoost(rb); // Call the base ApplyBoost method to apply the boost force
        float accumulatedVelocityMagnitude = previousSpeed + boostForce;
        Vector3 launchDirection = new Vector3(1, 1, 0).normalized;
        Vector3 finalForce = launchDirection * accumulatedVelocityMagnitude;

        rb.AddForce(finalForce, forceMode);

    }
}

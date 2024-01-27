using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Boost
{
    public float bounceForce = 5f;

    protected override void ApplyBoost(Rigidbody2D rb)
    {
        base.ApplyBoost(rb); // Call the base ApplyBoost method to apply the boost force

        // Apply additional bounce force
        rb.AddForce(new Vector3(1,1,0) * boostForce, ForceMode2D.Impulse);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallState : GravityObjectState
{
    public FreeFallState(Rigidbody2D rb2d) : base(rb2d)
    {
        //rotateSpeed = 2 * rotateSpeed;
    }

    public override void Rotate(Vector2? rotateTowards)
    {
        //base.Rotate(rotateTowards);
    }


}

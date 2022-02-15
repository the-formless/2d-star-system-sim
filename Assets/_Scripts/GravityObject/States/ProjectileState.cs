using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileState : GravityObjectState
{
    public ProjectileState(Rigidbody2D rb) : base(rb)
    {

    }

    public override void Rotate(Vector2? rotateTowards)
    {
        Vector2 velocity = rb2d.velocity.normalized;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb2d.transform.rotation = Quaternion.Slerp(rb2d.transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), rotateSpeed * Time.fixedDeltaTime);
    }
}

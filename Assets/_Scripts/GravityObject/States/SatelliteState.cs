using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteState : GravityObjectState
{
    private float satelliteSpeed;
    public SatelliteState(Rigidbody2D rb, float speed) : base(rb)
    {
        //rotateSpeed = 0.5f * rotateSpeed;
        //acceleration = 0;
        satelliteSpeed = speed;
    }

    public override void Move()
    {
        rb2d.velocity = direction * satelliteSpeed * Time.fixedDeltaTime * 2f;
    }

    public override void Rotate(Vector2? rotateTowards)
    {
        Vector2 velocity = rb2d.velocity.normalized;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        rb2d.transform.rotation = Quaternion.Slerp(rb2d.transform.rotation, (Quaternion.AngleAxis(angle, Vector3.forward)), rotateSpeed * Time.fixedDeltaTime);
    }
}

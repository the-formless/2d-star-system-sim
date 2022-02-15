using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityObjectState 
{
    protected Rigidbody2D rb2d;
    protected float acceleration = 0f;
    protected float rotateSpeed = 0f;
    protected Vector2 direction = Vector2.zero;

    public GravityObjectState(Rigidbody2D rb)
    {
        rb2d = rb;
    }

    public virtual void SetRoateSpeed(float val)
    {
        rotateSpeed = val;
    }
    public virtual void SetAcceleration(float val)
    {
        acceleration = val;
    }

    public virtual void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    public virtual void Move() 
    {
        rb2d.velocity += (direction * acceleration * Time.fixedDeltaTime);
    }
    public virtual void Rotate(Vector2? rotateTowards)
    {
        Vector2 bodyUp = rb2d.transform.up;
        Vector2 gravityUp = rotateTowards.HasValue ? -rotateTowards.Value : Vector2.zero;
        Quaternion rot = Quaternion.FromToRotation(new Vector3(bodyUp.x, bodyUp.y, 0), new Vector3(gravityUp.x, gravityUp.y, 0)) * rb2d.transform.rotation;
        rb2d.transform.rotation = Quaternion.Slerp(rb2d.transform.rotation, rot, rotateSpeed * Time.fixedDeltaTime);
    }


}

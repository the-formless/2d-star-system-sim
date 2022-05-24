using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharState 
{
    protected Rigidbody2D rb2d;
    protected GravityObject gravObj;
    protected Transform transform;
    public CharState(Transform tr, Rigidbody2D rb2d, GravityObject gravObj)
    {
        this.transform = tr;
        this.rb2d = rb2d;
        this.gravObj = gravObj;
    }

    public virtual void MoveCharacter(float direction, float moveSpeed, Vector2 bodyRight)
    {
        Type gravObjectStateType = gravObj.State.GetType();

        if(gravObjectStateType == typeof(FreeSpaceState))
        {
            //float rotate = direction * moveSpeed * Time.fixedDeltaTime;
            Vector2 bodyUp = Vector2.Perpendicular(bodyRight);
            Quaternion rot = Quaternion.FromToRotation(new Vector3(bodyUp.x, bodyUp.y, 0), new Vector3(bodyRight.x, bodyRight.y, 0) * direction) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, moveSpeed * 0.5f * Time.fixedDeltaTime);
        }
        else
        {

            transform.position += (Vector3)bodyRight * moveSpeed * direction * Time.fixedDeltaTime;
        }
    }

    public abstract void JumpCharacter(Vector2 bodyUp, float jumpSpeed);

    public abstract void JetPack(Vector2 bodyUp, float jetForce);

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveState : CharState
{
    public CharMoveState(Transform tr, Rigidbody2D rb2d, GravityObject gravObj) : base(tr, rb2d, gravObj)
    {

    }

    public override void JumpCharacter(Vector2 bodyUp, float jumpSpeed)
    {
        rb2d.velocity += bodyUp * jumpSpeed;
    }

    public override void JetPack(Vector2 bodyUp, float jetForce)
    {
        //rb2d.velocity += bodyUp * jetForce * Time.fixedDeltaTime;
    }
}

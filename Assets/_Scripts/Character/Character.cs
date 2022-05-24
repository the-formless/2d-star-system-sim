using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpSpeed = 20;
    public float jetForce = 10;
    public float jetFuel = 20;

    private Vector2 bodyRight;
    private Vector2 bodyUp;
    private Rigidbody2D rb2d;
    private GravityObject gravObject;
    private SpriteRenderer imageRend;

    private CharState state;
    private CharMoveState movingState;
    //private CharJumpState jumpState;
    private CharFallingState fallState;
    //private CharJetpackState jetState;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        gravObject = GetComponent<GravityObject>();
        imageRend = GetComponent<SpriteRenderer>();
        PrepareStates();
        state = movingState;
    }

    private void PrepareStates()
    {
        movingState = new CharMoveState(transform, rb2d, gravObject);
        //jumpState = new CharJumpState(transform, rb2d, gravObject);
        fallState = new CharFallingState(transform, rb2d, gravObject);
        //jetState = new CharJetpackState(transform, rb2d, gravObject);
    }

    // Update is called once per frame
    void Update()
    {
        bodyRight = transform.right;
        bodyUp = transform.up;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpCharacter();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            JetPack();
        }
    }

    private void DecrementJetFuel()
    {
        jetFuel -= 1 * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        MoveCharacter(moveDirection);

    }

    void MoveCharacter(float direction)
    {
        FlipSprite(direction);
        //transform.position += (Vector3) bodyRight * moveSpeed * direction * Time.fixedDeltaTime;
        state.MoveCharacter(direction, moveSpeed, bodyRight);
    }

    private void FlipSprite(float direction)
    {
        if (direction < 0)
        {
            imageRend.flipX = true;
        }
        if (direction > 0)
        {
            imageRend.flipX = false;
        }
    }

    void JumpCharacter()
    {
        //rb2d.velocity += bodyUp * jumpSpeed;
        state.JumpCharacter(bodyUp, jumpSpeed);
        state = fallState;
    }

    void JetPack()
    {
        //rb2d.velocity += bodyUp * jetForce * Time.fixedDeltaTime;
        if(jetFuel > 0)
        {
            state.JetPack(bodyUp, jetForce);
            DecrementJetFuel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            state = movingState;
        }
    }
}

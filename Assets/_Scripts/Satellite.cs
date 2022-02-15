using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{

    public float moveSpeed;
    public Vector2 direction;
    public Vector2 rotateAroundPoint;

    private Rigidbody2D rb2d;
    private void MoveTowardDirection(Vector2 dir)
    {
        //transform.Translate(dir.normalized * moveSpeed * Time.fixedDeltaTime);
        rb2d.velocity += dir.normalized * moveSpeed * Time.fixedDeltaTime;
    }

    public void RotateTowardsBody(Vector2 bodyPosition)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Calculate direction

        //move in direction
        direction = transform.right;
        MoveTowardDirection(direction);
        //rotate towards body
        //RotateTowardsBody(rotateAroundPoint);
    }
}

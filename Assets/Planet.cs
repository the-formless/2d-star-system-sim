using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Vector2 initialVelocity;
    public GravityBody star;
    public float planetSpeed = 1f;
    public float angleRange = 30f;


    private Rigidbody2D rigidbody2D;
    private SatelliteStateHelper satelliteHelper;
    private GravityBody planet;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = initialVelocity;
        satelliteHelper = new SatelliteStateHelper(rigidbody2D, angleRange);
        satelliteHelper.AssignGravityBody(star);
        planet = GetComponent<GravityBody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (satelliteHelper.CheckSatelliteStateTransition())
        {
            Move(satelliteHelper.satelliteDirection);
        }
    }

    private void Move(Vector2 satelliteDirection)
    {
        rigidbody2D.velocity = satelliteDirection * planetSpeed * Time.fixedDeltaTime;
    }

}

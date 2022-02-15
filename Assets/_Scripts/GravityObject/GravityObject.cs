using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public GravityBody gravityBody;
    public ProjectileState projectileState;
    public FreeFallState freefallState;
    public FreeSpaceState freeSpaceState;
    public SatelliteState satelliteState;
    public RestState restState;
    public Vector2 initialvelocity;
    public string stateName;
    public float satelliteSpeed;
    private GravityObjectState state;
    private Vector2? gravityDirection;
    private Rigidbody2D rb2d;

    public GravityObjectState State { get => state;}

   
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PrepareStates();
        state = freeSpaceState;
        rb2d.velocity = initialvelocity;
    }

    private void PrepareStates()
    {
        freefallState = new FreeFallState(rb2d);
        freeSpaceState = new FreeSpaceState(rb2d);
        satelliteState = new SatelliteState(rb2d, satelliteSpeed);
        projectileState = new ProjectileState(rb2d);
        restState = new RestState(rb2d);
    }

    // Update is called once per frame
    void Update()
    {
        stateName = state.GetType().ToString();
    }

    private void FixedUpdate()
    {
        Vector2? rotateDirection = Vector2.zero;
        if (gravityBody)
        {
            if (InSatelliteArea())
            {
                Vector2 plane = (Vector2)(transform.position - gravityBody.transform.position).normalized;
                Vector2 normal = new Vector2(plane.y, -plane.x);
                Vector2 velocity = rb2d.velocity.normalized;
                float angleBetween = Vector2.Angle(velocity, normal);
                if (angleBetween <= 30 || (angleBetween > 90 && (180 - angleBetween <= 30)) && rb2d.velocity.magnitude > 0)
                {
                    TransitionToState(satelliteState);
                    if((angleBetween > 90 && (180 - angleBetween <= 30)))
                    {
                        normal = -normal;
                    }
                    state.SetDirection(normal);
                    Debug.DrawRay(transform.position, (Vector3)normal);
                    rotateDirection = normal;
                    //Debug.Log(state.GetType());
                }
            }
            else
            {
                TransitionToState(freefallState);
                gravityDirection = (transform.position - gravityBody.transform.position).normalized * -1f;
                Debug.DrawRay(transform.position, (Vector3)gravityDirection);
                state.SetDirection(gravityDirection.Value);
                rotateDirection = gravityDirection;
            }
            if(((Vector2)transform.position - (Vector2)gravityBody.transform.position).magnitude > gravityBody.satelliteArea.y)
            {
                TransitionToState(freeSpaceState);
            }
            state.SetAcceleration(gravityBody.gravity);
        }
        else
        {
            rotateDirection = rb2d.velocity.normalized;
        }
        state.Move();
        state.Rotate(rotateDirection);
    }

    private void TransitionToState(GravityObjectState stateType)
    {
        Type currentState = state.GetType();
        if(currentState != stateType.GetType())
        {
            state = stateType;
        }
    }

    public void AssignGravityBody(GravityBody gravityBody)
    {
        state = projectileState;

        state.SetAcceleration(gravityBody.gravity);
        state.SetRoateSpeed(gravityBody.gravity);
        this.gravityBody = gravityBody;
    }

    public void ResignGravityBody()
    {
        state = freeSpaceState;
        state.SetAcceleration(0);
        state.SetRoateSpeed(rb2d.velocity.magnitude);
        state.SetDirection(rb2d.velocity.normalized);
        this.gravityBody = null;
    }

    bool InSatelliteArea()
    {
        if(gravityBody != null)
        {
            Vector2 position = transform.position;
            float distanceFromBody = (position - (Vector2)gravityBody.transform.position).magnitude;
            if(distanceFromBody >= gravityBody.satelliteArea.x && distanceFromBody <= gravityBody.satelliteArea.y)
            {
                return true;
            }
            return false;
        }
        return false;
    }

}

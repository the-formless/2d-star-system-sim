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
    public CharacterState characterState;
    public Vector2 initialvelocity;
    public string stateName;
    //public float satelliteSpeed;
    public float satelliteAngleRange;

    public bool character;

    private GravityObjectState state;
    private Vector2? gravityDirection;
    private Rigidbody2D rb2d;
    private SatelliteStateHelper stateHelper;
    private float gravityBodySpeed;

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
        satelliteState = new SatelliteState(rb2d, 0f);
        projectileState = new ProjectileState(rb2d);
        restState = new RestState(rb2d);
        characterState = new CharacterState(rb2d);
        stateHelper = new SatelliteStateHelper(rb2d, satelliteAngleRange);
    }

    // Update is called once per frame
    void Update()
    {
        stateName = state.GetType().ToString();
    }

    private void FixedUpdate()
    {
        HandleStates();
    }

    private void HandleStates()
    {
        Vector2? rotateDirection = Vector2.zero;
        if (gravityBody)
        {
            if (InSatelliteArea() && !character)
            {
                if (stateHelper.CheckSatelliteStateTransition())
                {
                    SimulateSatellite();
                    rotateDirection = stateHelper.satelliteDirection;
                }
            }
            else
            {
                SimulateFreeFall();
                rotateDirection = gravityDirection;
            }
            if (OutsideGravityField())
            {
                SimulateFreeSpace();
            }
            state.SetAcceleration(gravityBody.gravity);
            state.SetRoateSpeed(gravityBody.gravity * 5);
        }
        else
        {
            rotateDirection = rb2d.velocity.normalized;
            ResetSatelliteSpeed();
        }
        state.Move();
        state.Rotate(rotateDirection);
    }

    private void SimulateFreeSpace()
    {
        TransitionToState(freeSpaceState);
    }

    private bool OutsideGravityField()
    {
        return ((Vector2)transform.position - (Vector2)gravityBody.transform.position).magnitude > gravityBody.satelliteArea.y;
    }

    private void SimulateFreeFall()
    {
        if (character)
        {
            TransitionToState(characterState);
        }
        else
        {
            TransitionToState(freefallState);
        }
        gravityDirection = (transform.position - gravityBody.transform.position).normalized * -1f;
        Debug.DrawRay(transform.position, (Vector3)gravityDirection);
        state.SetDirection(gravityDirection.Value);
    }

    private void ResetSatelliteSpeed()
    {
        satelliteState.SatelliteSpeed = 0;
    }
    private void SimulateSatellite()
    {
        TransitionToState(satelliteState);
        if (satelliteState.SatelliteSpeed == 0)
        {
            satelliteState.SatelliteSpeed = stateHelper.CalculateSatelliteSpeed(rb2d.velocity.magnitude);
        }
        state.SetDirection(stateHelper.satelliteDirection);
        Debug.DrawRay(transform.position, (Vector3)stateHelper.satelliteDirection);
        //if (InSatelliteList() == false)
        //{
        //    gravityBody.AddObjectToSatelliteList(this.gameObject);
        //}
        rb2d.velocity += gravityBody.gameObject.GetComponent<Rigidbody2D>().velocity;
    }

    

    //private bool InSatelliteList()
    //{
    //    return gravityBody.satellites.Contains(this.gameObject);
    //}

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
        Planet p = gravityBody.gameObject.GetComponent<Planet>();
        stateHelper.AngleRange = p.angleRange;
        state = projectileState;
        state.SetAcceleration(gravityBody.gravity);
        state.SetRoateSpeed(gravityBody.gravity * 5f);
        //UpdateStatesValues(gravityBody.gravity, p);
        this.gravityBody = gravityBody;
        stateHelper.AssignGravityBody(gravityBody);
        //gravityBody.objectsOnBody.Add(this.gameObject);
        if (p)
        {
            gravityBodySpeed = p.planetSpeed;
        }
    }

    public void ResignGravityBody()
    {
        state = freeSpaceState;
        state.SetAcceleration(0);
        state.SetRoateSpeed(rb2d.velocity.magnitude);
        state.SetDirection(rb2d.velocity.normalized);
        //gravityBody.objectsOnBody.Remove(this.gameObject);
        //if (InSatelliteList())
        //{
        //    gravityBody.satellites.Remove(this.gameObject);
        //}
        this.gravityBody = null;
        satelliteState.SatelliteSpeed = 0f;
        stateHelper.ResignGravityBody();
        
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
            ResetSatelliteSpeed();
            return false;
        }
        ResetSatelliteSpeed();
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteStateHelper 
{
    public Vector2 satelliteDirection;

    private GravityBody gravityBody;
    private Rigidbody2D gravityBodyRigidBody;

    Rigidbody2D gravityObjectRigidBody;
    float angleRange;

    public float AngleRange { get => angleRange; set => angleRange = value; }

    public SatelliteStateHelper(Rigidbody2D rb, float angleRange)
    {
        gravityObjectRigidBody = rb;
        this.angleRange = angleRange;
    }
    public bool CheckSatelliteStateTransition()
    {
        Vector2 normal = CalculateNormal();
        Vector2 velocity = gravityObjectRigidBody.velocity.normalized;
        float angleBetween = Vector2.Angle(velocity, normal);
        if (angleBetween <= angleRange || (angleBetween > 90 && (180 - angleBetween <= angleRange)) && gravityObjectRigidBody.velocity.magnitude > 0)
        {
            if ((angleBetween > 90 && (180 - angleBetween <= 30)))
            {
                normal = -normal;
            }
            satelliteDirection = normal.normalized;
            return true;
        }
        satelliteDirection = Vector2.zero;
        return false;
    }

    private Vector2 CalculateNormal()
    {
        Vector2 plane = (Vector2)(gravityObjectRigidBody.transform.position - gravityBody.transform.position).normalized;
        Vector2 normal = new Vector2(plane.y, -plane.x);
        return normal;
    }

    public float CalculateSatelliteSpeed(float currentVelocity)
    {
        float dot = Vector2.Dot(gravityBodyRigidBody.velocity.normalized, satelliteDirection);
        float distance = Vector2.Distance(gravityBody.transform.position, gravityObjectRigidBody.transform.position);
        float vFactor = Mathf.Sqrt(1 / distance);
        float speedToReturn;
        if (dot <= 0.33)
        {
            speedToReturn = currentVelocity + currentVelocity * 3.33f * vFactor;
        }
        else if (dot <= 0.66)
        {
            speedToReturn = currentVelocity + currentVelocity * 6.66f * vFactor;
        }
        else
        {
            speedToReturn = currentVelocity + currentVelocity * 9.99f * vFactor;
        }
        return speedToReturn; // Mathf.Clamp(speedToReturn, gravityBodySpeed * 4, gravityBodySpeed * 7);
    }

    public void AssignGravityBody(GravityBody gb)
    {
        gravityBody = gb;
        gravityBodyRigidBody = gb.gameObject.GetComponent<Rigidbody2D>();
    }

    public void ResignGravityBody()
    {
        gravityBody = null;
        gravityBodyRigidBody = null;
    }
}

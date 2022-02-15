using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    //stores x:min satellite distance from center y:max satellite distance from center
    public Vector2 satelliteArea; 
    //gravity value of this body
    public float gravity;

    public GameObject[] satellites;
    GameObject[] objectsOnBody;

    private CircleCollider2D gravityField;
    private void AttractSatellites()
    {
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gravityField = this.gameObject.AddComponent<CircleCollider2D>();
        gravityField.radius = satelliteArea.y / (transform.localScale.x);
        gravityField.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, satelliteArea.x);
        Gizmos.DrawWireSphere(transform.position, satelliteArea.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GravityObject>())
        {
            collision.GetComponent<GravityObject>().AssignGravityBody(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GravityObject>())
        {
            collision.GetComponent<GravityObject>().ResignGravityBody();
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    //stores x: min satellite distance from center y: max satellite distance from center
    public Vector2 satelliteArea; 
    //gravity value of this body
    public float gravity;
    public bool isStar;

    //public List<GameObject> satellites;
    //public List<GameObject> objectsOnBody;

    private CircleCollider2D gravityField;
    
    // Start is called before the first frame update
    void Start()
    {
        //satellites = new List<GameObject>();
        //objectsOnBody = new List<GameObject>();
        if (!isStar)
        {
            gravityField = this.gameObject.AddComponent<CircleCollider2D>();
            gravityField.radius = satelliteArea.y / (transform.localScale.x);
            gravityField.isTrigger = true;
        }
        
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
        GravityObject inAtmospherObject = null;
        try
        {
            inAtmospherObject = collision.GetComponent<GravityObject>();
        }
        catch
        {
            Debug.Log("Not GravityObject");
        }
        if (inAtmospherObject != null)
        {
            inAtmospherObject.AssignGravityBody(this);
            if(!inAtmospherObject.character)
                inAtmospherObject.gameObject.GetComponent<TrailRenderer>().emitting = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GravityObject>())
        {
            collision.GetComponent<GravityObject>().ResignGravityBody();
            if(collision.GetComponent<GravityObject>().character == false)
                StartCoroutine(StopTrailAfter5secs(collision.GetComponent<TrailRenderer>()));
        }
    }

    IEnumerator StopTrailAfter5secs(TrailRenderer tr)
    {
        yield return new WaitForSeconds(0.5f);
        tr.emitting = false;
    }

    //public void AddObjectToSatelliteList(GameObject gameObject)
    //{
    //    if (objectsOnBody.Contains(gameObject))
    //    {
    //        satellites.Add(gameObject);
    //    }
    //}
}

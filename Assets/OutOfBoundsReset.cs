using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsReset : MonoBehaviour
{
    public Transform topRight;
    public Transform topLeft;
    public Transform bottomRight;
    public Transform bottomLeft;

    GameManager manager;
    Camera mainCam;
    //TrailRenderer trail;
    //bool resetStart = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        //trail = this.gameObject.GetComponent<TrailRenderer>();
        manager = mainCam.gameObject.GetComponent<GameManager>();
        topRight = manager.topRight;
        topLeft = manager.topLeft;
        bottomLeft = manager.bottomLeft;
        bottomRight = manager.bottomRight;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 viewportPosition = mainCam.WorldToScreenPoint(this.transform.position);
        if(viewportPosition.x > mainCam.pixelWidth + 2)// && resetStart != true)
        {
            //resetStart = true;
            //PauseTrail(true);
            viewportPosition.x = -2;
            transform.position = mainCam.ScreenToWorldPoint(viewportPosition);
            //0-top to bottom, 1-bottom to top, 2- left to right, 3-right to left
            //CyclePostion(3, mainCam.ScreenToWorldPoint(viewportPosition));
        }

        if (viewportPosition.y > mainCam.pixelHeight + 2)// && resetStart != true)
        {
            //resetStart = true;
            //PauseTrail(true);
            viewportPosition.y = -2;
            transform.position = mainCam.ScreenToWorldPoint(viewportPosition);
            //0-top to bottom, 1-bottom to top, 2- left to right, 3-right to left
            //CyclePostion(0, mainCam.ScreenToWorldPoint(viewportPosition));
        }
        
        if(viewportPosition.x < -2)// && resetStart != true)
        {
            //resetStart = true;
            //PauseTrail(true);
            viewportPosition.x = mainCam.pixelWidth + 2;
            transform.position = mainCam.ScreenToWorldPoint(viewportPosition);
            //0-top to bottom, 1-bottom to top, 2- left to right, 3-right to left
            //CyclePostion(2, mainCam.ScreenToWorldPoint(viewportPosition));
        }

        if (viewportPosition.y < -2)// && resetStart != true)
        {
            //resetStart = true;
            //PauseTrail(true);
            viewportPosition.y = mainCam.pixelHeight + 2;
            transform.position = mainCam.ScreenToWorldPoint(viewportPosition);
            //0-top to bottom, 1-bottom to top, 2- left to right, 3-right to left
            //CyclePostion(1, mainCam.ScreenToWorldPoint(viewportPosition));
        }
    }

    //private void CyclePostion(int v, Vector3 posFinal)
    //{
    //    //0-top to bottom, 1-bottom to top, 2- left to right, 3-right to left
    //    switch (v)
    //    {
    //        case 0:
    //            StartCoroutine(WaitBetweenSwitch(topRight.position, bottomRight.position, posFinal));
    //            break;
    //        case 1:
    //            //WaitBetweenSwitch();
    //            StartCoroutine(WaitBetweenSwitch(bottomRight.position, topRight.position, posFinal));
    //            break;
    //        case 2:
    //            //WaitBetweenSwitch();
    //            StartCoroutine(WaitBetweenSwitch(topLeft.position, topRight.position, posFinal));
    //            break;
    //        case 3:
    //            //WaitBetweenSwitch();
    //            StartCoroutine(WaitBetweenSwitch(topRight.position, topLeft.position, posFinal)); 

    //            break;
    //        default:
    //            break;
    //    }
    //}

    //IEnumerator WaitBetweenSwitch(Vector3 pos1, Vector3 pos2, Vector3 posFinal)
    //{
    //    yield return new WaitForSeconds(2);
    //    transform.position = pos1;
    //    yield return new WaitForSeconds(2);
    //    transform.position = pos2;
    //    yield return new WaitForSeconds(2);
    //    transform.position = posFinal;
    //    yield return new WaitForSeconds(2);
    //    PauseTrail(false);
    //    yield return new WaitForSecondsRealtime(5);
    //    resetStart = false;
    //}

    //void PauseTrail(bool v)
    //{
    //    trail.emitting = !v;
    //}


}

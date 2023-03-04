using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float rodPower = 1f;
    bool dragging = false;
    Vector3 dragStart;
    Vector3 dragEnd;
    public LineRenderer lineR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Our rod casting stuff
        if(Input.GetMouseButtonDown(0) && !dragging) //left mouse button clicked
        {
            dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
            Debug.Log("Mouse clicked");
            lineR.SetPosition(0, dragStart);
        }
        if(dragging)
        {
            lineR.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if(Input.GetMouseButtonUp(0) && dragging) //left mouse button reelesed
        {
            Debug.Log("Mouse up");
            dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

   
}
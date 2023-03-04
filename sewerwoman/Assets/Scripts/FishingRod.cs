using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float rodPower = 1f;
    bool dragging = false;
    Vector2 dragStart;
    Vector2 dragEnd;
    LineRenderer lineR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !dragging) //left mouse button
        {
            dragStart = (Vector2)Input.mousePosition;
        }

    }
}

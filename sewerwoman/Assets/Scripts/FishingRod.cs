using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public float rodPower = 1f;
    bool dragging = false;
    Vector3 dragStart;
    Vector3 dragEnd;
    public LineRenderer lineR;
    Ray ray;
    RaycastHit raycastHit;
    GameObject clickedObj = null;

    // Start is called before the first frame update
    void Start()
    {
        lineR.startWidth = 0.2f;
        lineR.endWidth = 0.4f;
        lineR.startColor = Color.green;
        lineR.endColor = Color.red;
        lineR.positionCount = 2;
        //lineR.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Our rod casting stuff
        if(Input.GetMouseButtonDown(0) && !dragging) //left mouse button clicked
        {
            dragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragStart.z = 0f;
            lineR.SetPosition(0, dragStart);
            lineR.SetPosition(1, dragStart);
            dragging = true;
            //Object getting stuff
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform.gameObject.CompareTag("MutantFish"))
                {
                    clickedObj = raycastHit.transform.gameObject; 
                }
                else{
                    clickedObj = null;
                }
            }
        }
        if(dragging)
        {
            dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragEnd.z = 0f;
            lineR.SetPosition(1, dragEnd);
        }
        if(Input.GetMouseButtonUp(0) && dragging) //left mouse button reelesed
        {
            if(clickedObj != null){
                throwObject();
            }
            dragging = false;
        }
    }

   void throwObject()
    {
        if(clickedObj.CompareTag("MutantFish"))
        {
            float power = Math.Abs(dragStart.x - dragEnd.x) + Math.Abs(dragStart.y - dragEnd.y);
            if(clickedObj != null){
            clickedObj.GetComponent<MutantFish>(); //Add .throwFish(dragEnd, power) or whatever to the end
            }
        }
        //throw the game object along the directon vector created from dragstart to dragend if it can be thrown, else losen it if its throwable but stuck, else nothing
    }
}
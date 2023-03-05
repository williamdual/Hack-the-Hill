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
    Vector2 dragStart;
    Vector2 dragEnd;
    public LineRenderer lineR;
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
            lineR.SetPosition(0, dragStart);
            lineR.SetPosition(1, dragStart);
            dragging = true;
            //Object getting stuff
            RaycastHit2D raycastHit = Physics2D.Raycast(dragStart, Vector2.zero);
            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.CompareTag("MutantFish"))
                {
                    clickedObj = raycastHit.collider.gameObject; 
                }
                else{
                    clickedObj = null;
                }
            }
        }
        if(dragging)
        {
            dragEnd = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            Debug.Log("Fish");
            float power = Math.Abs(dragStart.x - dragEnd.x) + Math.Abs(dragStart.y - dragEnd.y);
            Vector2 direction = dragEnd - dragStart;
            if(clickedObj != null){
                clickedObj.GetComponent<MutantFish>().fished_behavior(direction, power); //Add .throwFish(dragEnd, power) or whatever to the end
            }
        }
        //throw the game object along the directon vector created from dragstart to dragend if it can be thrown, else losen it if its throwable but stuck, else nothing
    }
}
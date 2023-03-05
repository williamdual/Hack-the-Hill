using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] float rodPowerCap = 1.0f;
    bool dragging = false;
    Vector2 dragStart;
    Vector2 mousePos;
    Vector2 dragEnd;
    public LineRenderer lineR;
    GameObject clickedObj = null;

    // Start is called before the first frame update
    void Start()
    {
        lineR.startWidth = 0.2f;
        lineR.endWidth = 0.4f;
        lineR.startColor = Color.red;
        lineR.endColor = Color.red;
        lineR.positionCount = 2;
        //lineR.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x >= transform.parent.gameObject.transform.position.x){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //Our rod casting stuff
        if(Input.GetMouseButtonDown(0) && !dragging) //left mouse button clicked
        {
            dragStart = mousePos;
            lineR.SetPosition(0, dragStart);
            lineR.SetPosition(1, dragStart);
            dragging = true;
            //Object getting stuff
            RaycastHit2D raycastHit = Physics2D.Raycast(dragStart, Vector2.zero);
            if (raycastHit.collider != null)
            {
                if (raycastHit.collider.gameObject.CompareTag("MutantFish") || raycastHit.collider.gameObject.CompareTag("Trash"))
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
            lineR.positionCount = 0;
            lineR.positionCount = 2;
            if (clickedObj != null){
                throwObject();
            }
            dragging = false;
            clickedObj = null;
        }
    }

   void throwObject()
    {
        float power = Math.Abs(dragStart.x - dragEnd.x) + Math.Abs(dragStart.y - dragEnd.y);
        float cappedPower = power > rodPowerCap ? rodPowerCap : power;
        Vector2 direction = dragEnd - dragStart;
        if(clickedObj.CompareTag("MutantFish"))
        {
            clickedObj.GetComponent<MutantFish>().fished_behavior(direction, cappedPower); //Add .throwFish(dragEnd, power) or whatever to the end
        }
        else if (clickedObj.CompareTag("Trash")) {
            clickedObj.GetComponent<Trash>().fished_behavior(direction, cappedPower, 17.0f);
        }
        //throw the game object along the directon vector created from dragstart to dragend if it can be thrown, else losen it if its throwable but stuck, else nothing
    }
}
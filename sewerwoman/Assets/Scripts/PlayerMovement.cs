using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    GameObject player;
    public Rigidbody2D rb;

    private bool gettingPushed = false;

    public bool isHittingGarbage;
    public bool depositedFish;

    Vector2 movement;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(!gettingPushed){
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void Hit(Vector2 direction){
        gettingPushed = true;
        Debug.Log(direction.x + ", " + direction.y);
        rb.isKinematic = false;
        rb.AddForce(new Vector3(direction.x, direction.y, 0) * 10, ForceMode2D.Impulse);
    }
}

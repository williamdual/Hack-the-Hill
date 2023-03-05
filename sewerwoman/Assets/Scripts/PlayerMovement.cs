using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;

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
        currentHealth = maxHealth;
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
        TakeDamage(10);
        gettingPushed = true;
        rb.isKinematic = false;
        rb.AddForce(new Vector3(direction.x, direction.y, 0) * 4, ForceMode2D.Impulse);
        StartCoroutine("ChangeToKinematic");
    }

    private IEnumerator ChangeToKinematic(){
        yield return new WaitForSeconds(1);
        rb.isKinematic = true;
        gettingPushed = false;
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        if(currentHealth <= 0){
            Die();
            return;
        }
        UpdateMaterial();
    }

    private void UpdateMaterial(){
        float val = ((float)(currentHealth)/(float)(maxHealth));
        transform.GetChild(1).GetComponent<RadioactiveScript>().UpdateValue(val);
    }

    private void Die(){
        Destroy(gameObject);
    }
}

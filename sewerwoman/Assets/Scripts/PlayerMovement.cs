using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject particles;
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
        if(!gettingPushed){
            //rb.isKinematic = true;
            TakeDamage(5);
            gettingPushed = true;
            rb.AddForce(new Vector3(direction.x, direction.y, 0).normalized * 1f, ForceMode2D.Impulse);
            StartCoroutine("ChangeToDynamic");
        }
    }

    private IEnumerator ChangeToDynamic(){
        yield return new WaitForSeconds(0.5f);
        gettingPushed = false;
        //rb.isKinematic = false;
    }

    public void TakeDamage(int amount){
        currentHealth -= amount;
        GameObject parts = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(parts, 1);
        GameObject.FindWithTag("AudioPlayer").GetComponent<AudioManagerScript>().PlaySound("PlayerDamage");
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
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().Lose();
        Destroy(gameObject);
    }
}

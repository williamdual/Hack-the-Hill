using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;

    public Rigidbody2D rb;
    public CircleCollider2D cc;

    int damage;
    int speed;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    public void initalize(int d, int s, Vector3 direction)
    {
        damage = d;
        speed = s;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Angler") && !collision.gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player.GetComponent<PlayerMovement>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

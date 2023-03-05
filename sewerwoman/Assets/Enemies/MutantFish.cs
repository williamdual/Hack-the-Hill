using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantFish : MonoBehaviour
{
    //physical aspects
    [SerializeField] int speed = 5;
    Vector2 velocity;
    bool isStunned;
    //references
    Rigidbody2D m_kbody;
    [SerializeField] GameObject target; //Whatever mutant fish is targetting

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0, 0);
        m_kbody = GetComponent<Rigidbody2D>();
        m_kbody.gravityScale = 0;
        isStunned = false;
    }

    void move() {
        Vector2 targetPos = target.transform.position;
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        velocity = direction * speed * Time.fixedDeltaTime;
        m_kbody.MovePosition(m_kbody.position + velocity);
        m_kbody.MoveRotation(Quaternion.LookRotation(direction));
    }

    public void fished_behavior(Vector2 direction, float magnitude) {
        isStunned = true;
        m_kbody.isKinematic = false;
        m_kbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
        StartCoroutine(after_fished_behavior());
    }

    private IEnumerator after_fished_behavior() {
        yield return new WaitForSeconds(0.5f);
        isStunned = false;
        m_kbody.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    { 
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().Hit(velocity.normalized);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStunned) {
            move();
        } 
    }
}

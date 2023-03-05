using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantFish : MonoBehaviour
{
    //physical aspects
    [SerializeField] int speed = 5;
    [SerializeField] float chargefactor = 2.5f;
    Vector2 velocity;
    //esoteric aspects
    [SerializeField] float chargedist = 2.5f;
    bool isStunned;
    bool isCharging;
    bool isPreparing;
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
        isCharging = false;
        isPreparing = false;
<<<<<<< Updated upstream
        target = GameObject.FindGameObjectsWithTag("Player")[0];
=======
        if (target == null) {
            target = GameObject.FindWithTag("Player");
        }
>>>>>>> Stashed changes
    }

    void angry_move() {
        Vector2 targetPos = target.transform.position;
        if (Vector2.Distance(targetPos, (Vector2)transform.position) <= chargedist) {
            StartCoroutine(beginCharge(targetPos));
            m_kbody.isKinematic = true;
            isPreparing = true;
        }
        else {
            Vector2 lineToTarget = targetPos - (Vector2)transform.position;
            Vector2 direction = lineToTarget.normalized;
            velocity = direction * speed * Time.fixedDeltaTime;
            m_kbody.MovePosition(m_kbody.position + velocity);
            /*
            if(direction.x > 0){
                if(direction.y > 0){
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, Mathf.Abs(transform.rotation.z) * -1);
                }
                else{
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, Mathf.Abs(transform.rotation.z));
                }
            }
            else{
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            */
            m_kbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    IEnumerator beginCharge(Vector2 targetPos) {
        yield return new WaitForSeconds(0.55f);
        m_kbody.isKinematic = false;
        m_kbody.angularVelocity = 0.0f;
        m_kbody.velocity = Vector2.zero;
        Vector2 lineToTarget = targetPos - (Vector2)transform.position;
        Vector2 direction = lineToTarget.normalized;
        velocity = direction * speed * chargefactor * Time.fixedDeltaTime;
        isPreparing = false;
        isCharging = true;
        StartCoroutine(endCharge());

    }

    void charge() {
        m_kbody.MovePosition(m_kbody.position + velocity);
    }

    IEnumerator endCharge() {
        yield return new WaitForSeconds(0.5f);
        m_kbody.angularVelocity = 0.0f;
        m_kbody.velocity = Vector2.zero;
        isCharging = false;
    }

    void happy_move() {

    }

    public void fished_behavior(Vector2 direction, float magnitude) {
        isStunned = true;
        m_kbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
        StartCoroutine(after_fished_behavior());
    }

    private IEnumerator after_fished_behavior() {
        yield return new WaitForSeconds(0.5f);
        isStunned = false;
        m_kbody.velocity = Vector2.zero;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    { 
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().Hit(transform.right.normalized);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStunned && !isCharging && !isPreparing) {
            angry_move();
        } 
        else if (isCharging) {
            charge();
        }
    }
}

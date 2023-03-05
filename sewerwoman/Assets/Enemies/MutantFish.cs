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
    bool goingLeft = true;
    float changeTimer;
    //references
    Rigidbody2D m_kbody;
    [SerializeField] GameObject target; //Whatever mutant fish is targetting

    bool happy = false;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0, 0);
        m_kbody = GetComponent<Rigidbody2D>();
        m_kbody.gravityScale = 0;
        isStunned = false;
        isCharging = false;
        isPreparing = false;
        target = GameObject.FindWithTag("Player");
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
            GetComponent<SpriteRenderer>().flipX = (transform.position.x > target.transform.position.x);
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
            /*
            if(direction.x > target.transform.position.x){
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            }
            else{
                if(direction.y > target.transform.position.y){
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, Mathf.Abs(transform.rotation.z) * -1);
                }
                else{
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, Mathf.Abs(transform.rotation.z));
                }
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
        else if(other.gameObject.tag == "Water" && !happy){
            happy = true;
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            isStunned = false;
            isCharging = false;
            isPreparing = false;
            changeTimer = Time.time + (Random.Range(1, 5));
            if(goingLeft){
                LookLeft();
            }
            else{
                LookRight();
            }
            Destroy(transform.GetChild(0).gameObject);
        }
        if(other.gameObject.tag == "Wall"){
            goingLeft = !goingLeft;
            if(goingLeft){
                LookLeft();
            }
            else{
                LookRight();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Water" && !happy){
            happy = true;
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            isStunned = false;
            isCharging = false;
            isPreparing = false;
            changeTimer = Time.time + (Random.Range(1, 5));
            if(goingLeft){
                LookLeft();
            }
            else{
                LookRight();
            }
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStunned && !isCharging && !isPreparing && !happy) {
            angry_move();
        } 
        else if (isCharging) {
            charge();
        }
        else if(happy){
            moveWater();
        }
    }

    void moveWater(){
        if(Time.time >= changeTimer){
            goingLeft = !goingLeft;
            if(goingLeft){
                LookLeft();
            }
            else{
                LookRight();
            }
            changeTimer = Time.time + (Random.Range(1, 5));
        }
        if(goingLeft){
            //velocity = new Vector2(Mathf.Sin(Time.time * Random.Range(0.1f, 1) * -1), Mathf.Sin(Time.time * Random.Range(-0.6f, 0.6f)));
            velocity = new Vector2(-1, Random.Range(-.9f, 1));
        }
        else{
            //velocity = new Vector2(Mathf.Sin(Time.time * Random.Range(0.1f, 1)), Mathf.Sin(Time.time * Random.Range(-0.6f, 0.6f)));
            velocity = new Vector2(1, Random.Range(-.9f, 1));
        }
        
        if(transform.position.y >= -3.4){
            velocity.y = Random.Range(-0.6f, -1.2f);
        }
        else if(transform.position.y <= -4.6){
            velocity.y = Random.Range(0.6f, 1.2f);
        }
        
        velocity = velocity * Random.Range(1, 2) * Time.fixedDeltaTime;
        m_kbody.MovePosition(m_kbody.position + velocity);
    }

    void LookLeft(){
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void LookRight(){
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

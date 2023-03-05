using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] float health = 100.0f;
    bool isShaking;
    Rigidbody2D m_kbody;

    // Start is called before the first frame update
    void Start()
    {
        isShaking = false;
        m_kbody = GetComponent<Rigidbody2D>();
    }

    public IEnumerator beginShake() {

    }

    void shake() {

    }

    void endShake() {

    }

    public void fished_behavior(Vector2 direction, float magnitude, float damage) {
        if (health <= 0) {
            m_kbody.isKinematic = false;
            m_kbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
            StartCoroutine(after_fished_behavior());
        }
        else {
            health -= damage;
        }
    }

    private IEnumerator after_fished_behavior() {
        yield return new WaitForSeconds(0.5f);
        m_kbody.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isShaking) {
            
        }
    }
}

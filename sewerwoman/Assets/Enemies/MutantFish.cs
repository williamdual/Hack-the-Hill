using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantFish : MonoBehaviour
{
    //physical aspects
    [SerializeField] int speed = 5;
    Vector2 velocity;
    //references
    Rigidbody2D m_kbody;
    [SerializeField] GameObject target; //Whatever mutant fish is targetting

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(0, 0);
        m_kbody = GetComponent<Rigidbody2D>();
    }

    void move() {
        Vector2 targetPos = target.transform.position;
        velocity = (targetPos - (Vector2)transform.position).normalized;
        m_kbody.MovePosition(m_kbody.position + velocity * speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }
}

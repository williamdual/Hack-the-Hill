using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] float health = 100.0f;
    bool isShaking;
    Rigidbody2D m_kbody;
    SpriteRenderer render;
    private Color alpha;

    // Start is called before the first frame update
    void Start()
    {
        isShaking = false;
        m_kbody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        alpha = render.color;
    }

    public void fished_behavior(Vector2 direction, float magnitude, float damage) {
        if (health <= 0) {
            m_kbody.isKinematic = false;
            m_kbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
            StartCoroutine(fadeOut(1.0f));
        }
        else {
            health -= damage;
            StartCoroutine(shake(0.2f));
        }
    }

    private IEnumerator shake(float time) {
        float counter = 0;
        float rotateup = time/2;
        Vector2 upvector = new Vector2(10, 10);
        Vector2 downvector = new Vector2(-10, -10);

        while (counter < time) {
            counter += Time.deltaTime;
            if (counter < rotateup) {
                //Debug.Log("up");
                m_kbody.MovePosition(m_kbody.position + upvector * Time.deltaTime);
            }
            else {
                //Debug.Log("down");
                m_kbody.MovePosition(m_kbody.position + downvector * Time.deltaTime);
            }
            yield return null;
        }
    }

    IEnumerator fadeOut(float time) {
        float counter = 0;

        while (counter < time) {
            counter += Time.deltaTime;
            alpha.a = Mathf.Lerp(1, 0, counter / time);
            render.color = alpha;
            if (alpha.a <= 1/12) {
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] GameObject particles;
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
        damage = magnitude * 1.25f;
        //shh its scuffed its 5 am debugging

        GameObject.FindWithTag("AudioPlayer").GetComponent<AudioManagerScript>().PlaySound("Garbage");
        GameObject parts = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(parts, 1);
        if (health <= 0) {
            m_kbody.isKinematic = false;
            m_kbody.AddForce(direction * 1.25f, ForceMode2D.Impulse);
            StartCoroutine(fadeOut(1.0f));
        }
        else {
            health -= damage;
            StartCoroutine(shake(0.2f, damage));
        }
    }

    private IEnumerator shake(float time, float shakeAmount) {
        float counter = 0;
        float rotateup = time/2;
        shakeAmount *= 2.5f;
        Vector2 upvector = new Vector2(shakeAmount, shakeAmount);
        Vector2 downvector = new Vector2(-shakeAmount, -shakeAmount);

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

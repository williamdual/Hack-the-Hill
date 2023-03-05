using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [SerializeField] LineRenderer lineR;
    [SerializeField] float radius = 5.0f;
    Rigidbody2D parentbody;
    Vector2 offsetPos;
    Vector2 offsetNeg;
    Vector2 offsetToUse;

    // Start is called before the first frame update
    void Start()
    {
        lineR.startWidth = 0.05f;
        lineR.endWidth = 0.05f;
        lineR.startColor = Color.gray;
        lineR.endColor = Color.gray;
        lineR.positionCount = 2;
        parentbody = GetComponentInParent<Rigidbody2D>();
        offsetPos = new Vector2(0.409f, 0.375f) * 2;
        offsetNeg = new Vector2(-0.409f, 0.375f) * 2;
    }

    public 

    void renderLine() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 originPos = parentbody.position;
        if (Vector2.Distance(mousePos, originPos) <= radius) {
            lineR.startWidth = 0.05f;
            lineR.endWidth = 0.05f;
            if (mousePos.x >= transform.parent.gameObject.transform.position.x) {
                lineR.SetPosition(0, originPos + offsetPos);
            }
            else {
                lineR.SetPosition(0, originPos + offsetNeg);
            }
            lineR.SetPosition(1, mousePos);
        }
        else {
            lineR.startWidth = 0.0f;
            lineR.endWidth = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        renderLine();
    }
}

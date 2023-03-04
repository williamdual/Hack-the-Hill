using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOffset : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] float speed;
    [SerializeField] float amplitude;

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        transform.position = (transform.up * Mathf.Sin(Time.time * speed + yOffset) * amplitude) + initialPos;
    }
}

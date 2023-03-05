using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRadioactive : MonoBehaviour
{

    private Material material;
    float t = 0;
    bool decreasing = false;
    float radioactivity = 0;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(t >= 1)
        {
            decreasing = true;
        }
        
        if (decreasing)
        {
            if(t >= .45 && t <= .6){
                t -= Time.deltaTime * 0.33f;
            }
            else{
                t -= Time.deltaTime;
            }
        }
        else
        {
            if(t >= .45 && t <= .6){
                t += Time.deltaTime * 0.33f;
            }
            else{
                t += Time.deltaTime;
            }
        }

        if(t <= 0 && decreasing)
        {
            decreasing = false;
        }

        radioactivity = Mathf.Lerp(0, 1, t);
        material.SetFloat("_RadioactiveAmount", radioactivity);
    }
}

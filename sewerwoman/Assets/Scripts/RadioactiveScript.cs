using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioactiveScript : MonoBehaviour
{

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    public void UpdateValue(float newValue){
        material.SetFloat("_RadioactiveAmount", 1 - newValue);
    }
}

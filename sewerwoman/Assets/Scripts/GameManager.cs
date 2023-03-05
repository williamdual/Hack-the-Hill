using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject fish;
    [SerializeField] float spawnInterval;
    [SerializeField] Transform[] spawnPoints;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= spawnTimer){
            SpawnFish();
            spawnTimer = Time.time + spawnInterval;
        }
    }

    void SpawnFish(){
        Instantiate(fish, spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }
}

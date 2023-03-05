using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int numGarbage = 0;
    private int maxGarbage = 4;
    [SerializeField] GameObject fish;
    [SerializeField] float spawnInterval;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int[] illegalIndices;
    float spawnTimer;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject loseText;
    [SerializeField] GameObject winText;

    private bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = Time.time + spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= spawnTimer && !won){
            SpawnFish();
            spawnTimer = Time.time + spawnInterval;
        }
    }

    void SpawnFish(){
        int newRand = -1;
        bool exit = false;
        while(!exit){
            newRand = Random.Range(0, spawnPoints.Length);
            if(illegalIndices[newRand] == 0){
                exit = true;
            }
        }
        Instantiate(fish, spawnPoints[newRand]);
    }

    public void RemoveGarbage(int newIndex){
        numGarbage++;
        illegalIndices[newIndex] = 1;
        if(numGarbage == maxGarbage){
            StartCoroutine("Win");
        }
    }

    public void AddGarbage(Transform newTransform, int newIndex){
        spawnPoints[newIndex] = newTransform;
    }

    private IEnumerator Win(){
        if(!won){
            won = true;
            GameObject.FindWithTag("AudioPlayer").GetComponent<AudioManagerScript>().PlaySound("Victory");
            yield return new WaitForSeconds(1.5f);
            ui.SetActive(true);
            loseText.SetActive(false);
            GameObject.FindWithTag("Angler").gameObject.SetActive(false);
            //pop up UI, play win sound
        }
    }

    public void Lose(){
        if(!won){
            won = true;
            //GameObject.FindWithTag("Angler").gameObject.SetActive(false);
            ui.SetActive(true);
            winText.SetActive(false);   
        }
        
    }
}

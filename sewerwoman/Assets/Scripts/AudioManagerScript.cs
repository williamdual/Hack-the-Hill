using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource[] BeamCharge;
    public AudioSource[] BeamFire;
    public AudioSource[] Garbage;
    public AudioSource[] FishHappy;
    public AudioSource[] FishHit;
    public AudioSource[] PlayerDamage;
    public AudioSource[] Victory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        AudioSource[] selected;
        switch (name)
        {
            case "BeamCharge":
                selected = BeamCharge; break;
            case "BeamFire":
                selected = BeamFire; break;
            case "Garbage":
                selected = Garbage; break;
            case "FishHappy":
                selected = FishHappy; break;
            case "FishHit":
                selected = FishHit; break;
            case "PlayerDamage":
                selected = PlayerDamage; break;
            case "Victory":
                selected = Victory; break;
            default:
                selected = Victory;
                Debug.Log("Sound not found"); break;
        }
        selected[Random.Range(0, selected.Count()-1)].Play();
        /*
        if (!AudioPlayer.isPlaying)
        {
            AudioPlayer.Play();
        }
        */
    }
}

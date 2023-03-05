using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AnglerScript : MonoBehaviour
{
    GameObject player;

    //Bullet stuff
    public GameObject bullet;
    public int bulletsPerSecond = 3;
    public int bulletDamage = 1;
    public int bulletSpeed = 10;
    float curTime;

    //Beam stuff
    public int beamDamage = 20;
    bool aimingBeam = false;
    bool shootingBeam = false;
    bool beamOnCooldown = false;
    float thinBeam = 0.1f;
    float thickBeam = 0.5f;
    public LineRenderer lineR;
    public float timeItTakesToAim = 5f;
    public float timeItTakesToShoot = 0.8f;
    public float timeItTakesToCooldown = 1.5f;
    float timeToAim = 0f;
    float timeToShoot = 0f;
    float timeToCooldown = 0f;
    Vector3 positionToShoot;
    Vector3 shootingPoint;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        lineR.startColor = Color.red;
        lineR.endColor = Color.red;
        lineR.startWidth = thinBeam;
        lineR.endWidth = thinBeam;
        lineR.positionCount = 2;
        shootingPoint = transform.position + new Vector3(0, 2.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        BulletLogic();
        checkBeam();
        BeamLogic();

    }

    void BulletLogic()
    {   
        curTime += Time.deltaTime;
        if (curTime > 1.0f / bulletsPerSecond)
        {
            Debug.Log(curTime);
            Vector3 bulletPath = new Vector3(Random.Range(-1f,1f), Random.Range(-1f,-0.2f),0);
            GameObject bull = Instantiate(bullet, transform.position, transform.rotation);
            bull.transform.position = shootingPoint + new Vector3(Random.Range(-2.5f, 2.5f), 0, 0);
            bull.GetComponent<Bullet>().initalize(bulletDamage, bulletSpeed, bulletPath);
            curTime = 0;
        }
    }
    void BeamLogic()
    {
        if(beamOnCooldown)
        {
            timeToCooldown -= Time.deltaTime;
            if(timeToCooldown < timeItTakesToCooldown * 0.7)
            {
                lineR.endColor = Color.red;
                lineR.positionCount = 0;
                lineR.positionCount = 2;
            }
            if (timeToCooldown <= 0f)
            {
                beamOnCooldown = false;
            }
        }
        if (aimingBeam)
        {
            timeToAim -= Time.deltaTime;
            positionToShoot = player.transform.position;
            lineR.SetPosition(1, positionToShoot);
          
            if (timeToAim <= 0)
            {
                aimingBeam = false;
                timeToShoot = timeItTakesToShoot;
                shootingBeam = true;
            }
        }
        else if (shootingBeam)
        {
            timeToShoot -= Time.deltaTime;
            if (timeToShoot <= 0)
            {
                lineR.endWidth = thickBeam;
                shootingBeam = false;
                ShootBeam();
            }
        }
    }
    void checkBeam()
    {
        if (!aimingBeam && !shootingBeam && !beamOnCooldown)
        {
            PrepareBeam();
        }
        //if statement with criteria like "is player fishing garbage" if so will start the shooting sequence: calls  PrepareBeam()
    }
    void PrepareBeam()
    {
        timeToAim = timeItTakesToAim;
        aimingBeam = true;
        lineR.SetPosition(0, shootingPoint);
    }
    void ShootBeam()
    {
        lineR.SetPosition(1, (positionToShoot - shootingPoint) * 5);
        lineR.startColor = Color.green;
        lineR.endColor = Color.green;
        hit = Physics2D.Raycast(shootingPoint, positionToShoot - shootingPoint);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.CompareTag("Player")) 
            {
                player.GetComponent<PlayerMovement>().TakeDamage(beamDamage);
            }
        }
        beamOnCooldown = true;
        timeToCooldown = timeItTakesToCooldown;

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    enum WeaponID {knife, rifle};
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    
    public int knifeCount = 1;
    
    public Animator knifeAnimation;

    //Objects the player can use

    public GameObject[] inventory;

    public GameObject fist;
    public GameObject knife;
    public GameObject pistol;
    public GameObject rifle;

    public GameObject itemInHand;
    private WeaponID itemNumber = WeaponID.rifle; 
    
    


    void Start()
    {
        knifeAnimation = GetComponent<Animator>();


        inventory = new GameObject[] {knife, rifle };
        itemNumber = WeaponID.rifle;
        itemInHand = inventory[(int) itemNumber];
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = .2f;
        laserLine.endWidth = .1f;
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if ((int)itemNumber < inventory.Length - 1)
            {
                Debug.Log(itemNumber);
                inventory[(int)itemNumber].SetActive(false);
                itemNumber++;
                inventory[(int)itemNumber].SetActive(true);
                itemInHand = inventory[(int)itemNumber];
            }
            else
            {
                Debug.Log(itemNumber);
                inventory[(int)itemNumber].SetActive(false);
                itemNumber = 0;
                inventory[(int)itemNumber].SetActive(true);
                itemInHand = inventory[(int)itemNumber];
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (itemNumber > 0)
            {
                Debug.Log(itemNumber);
                inventory[(int)itemNumber].SetActive(false);
                itemNumber--;
                inventory[(int)itemNumber].SetActive(true);
                itemInHand = inventory[(int)itemNumber];
            }
            else
            {
                Debug.Log(itemNumber);
                inventory[(int)itemNumber].SetActive(false);
                itemNumber = (WeaponID)inventory.Length - 1;
                inventory[(int)itemNumber].SetActive(true);
                itemInHand = inventory[(int)itemNumber];
            }
        }
        if (Input.GetButtonDown("Fire1")){
            switch (itemNumber)
            {
                case WeaponID.rifle:
                    FireGun();
                    break;
                case WeaponID.knife:
                    FireKnife();
                    break;
            }
        }
    }
    private IEnumerator ShotEffect()
    {
        gunAudio.Play();

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }

    void FireGun()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);


            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                if (hit.transform.tag == "Enemy")
                {
                    EnemyController theEnemy = hit.collider.GetComponent<EnemyController>();

                    theEnemy.Damage(gunDamage);


                }

            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }
    void FireKnife()
    {
        
        knifeAnimation.Play("KnifeAnimation");

    }
}

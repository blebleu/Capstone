using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    public Transform player;
    public Transform seekLocation;
    public GameObject[] patrolSpots;
    EnemyController eCon;

    public enum EnemyStates { Patrol, Seeking, Searching, Attacking, Retreating };
    
    public EnemyStates enemyState = EnemyStates.Patrol;
    EnemyController enemyScript;
    public int currentPostion = 0;
    public float searchLength = 0;
    private LineRenderer laserLine;
    Ray enemyRay;
    public GameObject enemyGun;
    private float enemyRange = 30;
    public float FOV = 65;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    [SerializeField] private float fireRate = 0;
    [SerializeField] private float lastSeen = 0;


    //Vars to help the enemy from getting stuck
    public List<Vector3> enemyPos = new List<Vector3>();
    float checkPos = 5f;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

        enemyPos.Add(gameObject.transform.position);
        EnemyController enemyScript = GetComponent<EnemyController>();
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = .2f;
        laserLine.endWidth = .1f;
        eCon = GetComponent<EnemyController>();
    }
    private void Update()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();


        CanSeePlayer();
        if(eCon.currentHealth <= 1)
        {
            enemyState = EnemyStates.Retreating;
        }
        
        if (enemyState == EnemyStates.Patrol || enemyState == EnemyStates.Retreating)
        {
            agent.isStopped = false;
            agent.destination = patrolSpots[currentPostion].gameObject.transform.position;
        }
        else if (enemyState == EnemyStates.Seeking)
        {
            agent.isStopped = false;
            agent.destination = seekLocation.position;
            float distance = Vector3.Distance(gameObject.transform.position - gameObject.transform.position.y * Vector3.up, seekLocation.position - seekLocation.position.y *Vector3.up);
            if(distance <= .3)
            {
                
                Renderer rend = seekLocation.GetComponent<Renderer>();
                EnemyCameras e = seekLocation.GetComponent<EnemyCameras>();
                rend.material = e.white;
                searchLength = 10;
                enemyState = EnemyStates.Searching;
            }


            //Get the enemy unstuck if he is stuck in a pos
            checkPos -= Time.deltaTime;
            if(checkPos <= 0)
            {
                checkPos = 5;
                enemyPos.Add(gameObject.transform.position);
                float distanceEnemyPos = Vector3.Distance(enemyPos[enemyPos.Count -1] - enemyPos[enemyPos.Count - 1].y * Vector3.up, enemyPos[enemyPos.Count - 2] - enemyPos[enemyPos.Count - 2].y * Vector3.up);
                Debug.Log(distanceEnemyPos);
                if (distanceEnemyPos <= 1.5)
                {
                    searchLength = 10;
                    enemyState = EnemyStates.Searching;
                }

            }
        }
        else if(enemyState == EnemyStates.Searching)
        {
            
            agent.isStopped = true;
            gameObject.transform.Rotate(0, 1, 0);
            searchLength -= Time.deltaTime;
            if(searchLength <= 0)
            {
                enemyState = EnemyStates.Patrol;
                agent.isStopped = false;
            }
            
        }
        else if (enemyState == EnemyStates.Attacking)
        {
            agent.isStopped = false;
            agent.destination = player.transform.position;
            lastSeen -= Time.deltaTime;
            if (lastSeen <= 0)
            {
                searchLength = 10;
                enemyState = EnemyStates.Searching;
                
            }
        }
        

        //agent.destination = player.position;
    }
    private void OnTriggerEnter(Collider other)
    {

        //Patrol Object Positions
        if (other.gameObject == patrolSpots[currentPostion])
        {
            if (currentPostion >= patrolSpots.Length - 1)
            {
                currentPostion = 0;
            }
            else if (currentPostion < patrolSpots.Length - 1)
            {
                currentPostion++;
                Debug.Log(currentPostion);
            }
        }
    }

    public void CameraTriggered(GameObject other)
    {
        if (enemyState != EnemyStates.Attacking)
        {
            enemyState = EnemyStates.Seeking;
            seekLocation = other.transform;
            Debug.Log(other.transform.position);
        }
    }
    private IEnumerator ShotEffect()
    {
        

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }

    private void CanSeePlayer()
    {
        Vector3 rayOrigin = enemyGun.transform.position;
        laserLine.SetPosition(0, rayOrigin);
        fireRate -= Time.deltaTime;   
        RaycastHit hit;
        Vector3 rayDirection = player.transform.position - rayOrigin;
        Debug.DrawRay(rayOrigin, rayDirection, Color.red);
        if ((Vector3.Angle(rayDirection, transform.forward)) <= FOV * 0.5f)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, enemyRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    lastSeen = 10;
                    enemyState = EnemyStates.Attacking;
                    laserLine.SetPosition(1, hit.point);

                    PlayerController thePlayer = hit.collider.GetComponent<PlayerController>();

                    if (fireRate <= 0)
                    {

                        StartCoroutine(ShotEffect());
                        thePlayer.health -= 1;
                        fireRate = 2;
                    }
                }
            }
        }

    }
}



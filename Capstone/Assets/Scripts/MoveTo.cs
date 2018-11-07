using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    public Transform player;
    public Transform spyCamera;
    public GameObject[] patrolSpots;

    public enum EnemyStates { Patrol, Seeking, Searching, Attacking, Retreating };
    
    public EnemyStates enemyState = EnemyStates.Patrol;
    EnemyController enemyScript;
    public int currentPostion = 0;
    public float searchLength = 0;
    private LineRenderer laserLine;
    Ray enemyRay;
    public GameObject enemyGun;
    private float enemyRange = 15;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    [SerializeField] private float fireRate = 0;

    [SerializeField] private float lastSeen = 0;

    private void Start()
    {
        EnemyController enemyScript = GetComponent<EnemyController>();
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = .2f;
        laserLine.endWidth = .1f;
    }
    private void FixedUpdate()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        Vector3 rayOrigin = enemyGun.transform.position;
        RaycastHit hit;
        laserLine.SetPosition(0, rayOrigin);
        fireRate -= Time.deltaTime;
        enemyRay = new Ray(rayOrigin, rayOrigin * 15);
        Debug.DrawRay(rayOrigin, enemyGun.transform.right * 15, Color.red);
        if (Physics.Raycast(rayOrigin, enemyGun.transform.right, out hit, enemyRange))
        {

            if (hit.transform.tag == "Player")
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
        if (enemyState == EnemyStates.Patrol)
        {
            agent.isStopped = false;
            agent.destination = patrolSpots[currentPostion].gameObject.transform.position;
        }
        else if (enemyState == EnemyStates.Seeking)
        {
            agent.isStopped = false;
            agent.destination = spyCamera.position;
            float distance = Vector3.Distance(gameObject.transform.position - gameObject.transform.position.y * Vector3.up, spyCamera.position - spyCamera.position.y *Vector3.up);
            if(distance <= .3)
            {
                
                Renderer rend = spyCamera.GetComponent<Renderer>();
                rend.material.color = Color.gray;
                searchLength = 10;
                enemyState = EnemyStates.Searching;
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
        enemyState = EnemyStates.Seeking;
        spyCamera = other.transform;
        Debug.Log(other.transform.position);
    }
    private IEnumerator ShotEffect()
    {
        

        laserLine.enabled = true;

        yield return shotDuration;

        laserLine.enabled = false;
    }
}

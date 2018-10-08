using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{

    public Transform player;
    public Transform spyCamera;
    public GameObject[] patrolSpots;

    enum EnemyStates { Patrol, Seeking, Attacking, Retreating };
    EnemyStates enemyState = EnemyStates.Patrol;
    EnemyController enemyScript;
    public int currentPostion = 0;
    private void Start()
    {
        EnemyController enemyScript = GetComponent<EnemyController>();
    }
    private void Update()
    {
        /***if (enemyScript.currentState = (int)enemyState.Patrol)
        {

        }
        ***/

        
    }


    private void FixedUpdate()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        if (enemyState == EnemyStates.Patrol)
        {
            agent.destination = patrolSpots[currentPostion].gameObject.transform.position;
        }
        else if (enemyState == EnemyStates.Seeking)
        {
            agent.destination = spyCamera.position;
            if((spyCamera.position.x - gameObject.transform.position.x <= .3) && (spyCamera.position.z - gameObject.transform.position.z <= .3))
            {
                enemyState = EnemyStates.Patrol;
                Renderer rend = spyCamera.GetComponent<Renderer>();
                rend.material.color = Color.gray;
                
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
}

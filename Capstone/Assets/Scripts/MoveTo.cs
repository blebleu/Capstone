using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {


    public Transform player;
    public Transform camera;
    enum EnemyStates { Patrol, Seeking, Attacking, Retreating };
    EnemyStates enemyState;
    EnemyController enemyScript;

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

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = player.position;
    }
}

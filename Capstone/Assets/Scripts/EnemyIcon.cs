using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour {
    public Material[] enemyTextures = new Material[5];
    public GameObject enemy;
    MoveTo enemyScript;
    Material enemyMat;
        // Use this for initialization
	void Start () {
        enemyMat = GetComponent<Renderer>().material;
        enemyScript = enemy.GetComponent<MoveTo>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (enemyScript.enemyState)
        {

            case MoveTo.EnemyStates.Patrol:
                enemyMat = enemyTextures[0];
                Debug.Log("Called Change Mat 1");
                break;
            case MoveTo.EnemyStates.Seeking:
                enemyMat = enemyTextures[1];
                Debug.Log("Called Change Mat 2");
                break;
            case MoveTo.EnemyStates.Searching:
                enemyMat = enemyTextures[2];
                Debug.Log("Called Change Mat 3");
                break;
            case MoveTo.EnemyStates.Attacking:
                enemyMat = enemyTextures[3];
                Debug.Log("Called Change Mat 4");
                break;
            case MoveTo.EnemyStates.Retreating:
                enemyMat = enemyTextures[4];
                Debug.Log("Called Change Mat 5");
                break;

        }
        GetComponent<Renderer>().material = enemyMat;
	}
}

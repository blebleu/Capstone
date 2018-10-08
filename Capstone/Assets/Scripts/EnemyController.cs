using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //The Enemy's current health point total

    public int currentHealth = 5;
    enum EnemyStates { Patrol, Seeking, Attacking, Retreating };
    EnemyStates enemyState;
    public int currentState;


    private void Start()
    {
        EnemyStates enemyState = EnemyStates.Patrol;
        int currentState = (int)enemyState;
        
    }










    public void Damage(int damageAmount)

    {

        //subtract damage amount when Damage function is called

        currentHealth -= damageAmount;

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }

}

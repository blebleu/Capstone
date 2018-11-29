using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour {
    private float enemyKnifed = 0;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.transform.tag == "Enemy")
        {
            Debug.Log("Object hit enemy");
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyKnifed <= 0) { 
            enemyController.Damage(1);
                enemyKnifed = 1f;
            }
        }
    }


    private void Update()
    {
        if (enemyKnifed >= 0)
        {
            enemyKnifed -= Time.deltaTime;
        }
    }
}

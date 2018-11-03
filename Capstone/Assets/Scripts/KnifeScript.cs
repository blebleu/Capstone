using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.transform.tag == "Enemy")
        {
            Debug.Log("Object hit enemy");
            EnemyController enemyController = other.GetComponent<EnemyController>();

            enemyController.Damage(1);
        }
    }
  
}

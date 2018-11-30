using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetection : MonoBehaviour {
    public float lifeTime = 2; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            MoveTo temp = other.GetComponent<MoveTo>();
            temp.enemyState = MoveTo.EnemyStates.Seeking;

            Vector3 tempVect3 = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);


            Transform shotLocation = gameObject.transform;
            shotLocation.position = tempVect3;
            

            temp.seekLocation = shotLocation;
        }
    }
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

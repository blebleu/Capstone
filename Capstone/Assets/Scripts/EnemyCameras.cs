using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameras : MonoBehaviour {
    public GameObject[] enemiesToCall;
    Renderer rend;
    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material.color = Color.black;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            for(int i = 0; i < enemiesToCall.Length; i++)
            {
                MoveTo enemyScipt = enemiesToCall[i].GetComponent<MoveTo>();
                enemyScipt.CameraTriggered(gameObject);
                Debug.Log("triggered 2");

               
            }
            Renderer rend = gameObject.GetComponent<Renderer>();
            rend.material.color = Color.white;
        }
       

       
    }
}

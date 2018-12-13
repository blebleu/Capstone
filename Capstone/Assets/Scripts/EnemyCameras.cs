using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameras : MonoBehaviour {
    public GameObject[] enemiesToCall;
    Renderer rend;
    public Material white;
    public Material red;
    void Start()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();
        rend.material = white;
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
            rend.material = red;
        } 
    }
}

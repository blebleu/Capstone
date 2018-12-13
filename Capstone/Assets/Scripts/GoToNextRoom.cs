using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextRoom : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(68, 9, -60);
        }
    }
}

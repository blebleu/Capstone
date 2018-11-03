using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delOnRun : MonoBehaviour {
    Renderer rend;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = false;
    }

}

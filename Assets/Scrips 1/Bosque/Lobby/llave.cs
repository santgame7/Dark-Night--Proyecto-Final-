using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llave : MonoBehaviour
{
    public Puerta puertaOpen;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            puertaOpen.isUnlocked = true;
        }
        Destroy(gameObject); 
    }
}

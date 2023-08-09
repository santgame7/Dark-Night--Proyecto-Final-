using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Transform puerta;
    public float puertaSpeed = 1f;
    Vector3 targetPosition;
    public Transform open;
    public Transform closed;
    float time;
    public bool isUnlocked = true;

    private void Start()
    {
        targetPosition = closed.position;
    }
    private void Update()
    {
        if (isUnlocked && puerta.position != targetPosition)
        {
            puerta.transform.position = Vector3.Lerp(puerta.transform.position, targetPosition, time);
            time += Time.deltaTime * puertaSpeed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Es para que la se habra
        if (other.tag == "Player")
        {
            targetPosition = open.position;
            time = 0;
        }
            
    }

    void OnTriggerExit(Collider other)
    {
        //Es para que la se cierre
        if (other.tag == "Player")
        {
            targetPosition = closed.position;
            time = 0;
        }

    }
}

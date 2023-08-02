using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : Interactuable
{
    [SerializeField] bool abierto;
    [SerializeField] private Animator animo;
    Collider coliderPuerta;
    // Start is called before the first frame update
    void Start()
    {
        abierto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (abierto)
        {
            animo.SetBool("Abrio", true);
        }
        else
        {
            animo.SetBool("Abrio", false);
        }
    }

    public override void Interact()
    {
        base.Interact();

        abierto = !abierto;
    }
}

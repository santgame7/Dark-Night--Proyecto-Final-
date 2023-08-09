using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : Interactuable
{
    [SerializeField] bool abierto;
    [SerializeField] private Animator animo;
    Collider coliderPuerta;

    [SerializeField] string llaveRequerida;
    [SerializeField] string llaveID;
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

        // Buscar la llave requerida por su nombre en el escenario
        GameObject llaveRequerida = GameObject.Find(llaveID);

        // Verificar si se encontró la llave requerida
        if (llaveRequerida != null || llaveID == "lock")
        {
            Debug.Log("No tienes la llave necesaria para abrir esta puerta.");
        }
        else
        {
            // Si se encontró la llave requerida, se puede abrir o cerrar la puerta
            abierto = !abierto;
        }
    }
}

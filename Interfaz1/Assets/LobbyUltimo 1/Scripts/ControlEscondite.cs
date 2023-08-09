using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEscondite : Interactuable
{
    [SerializeField] bool abierto;
    [SerializeField] private Animator anima;

    [SerializeField] private GameObject puertas;
    Collider coliderPuerta;
    Collider[] coliders;

    [SerializeField] LayerMask capaJugador;
    [SerializeField] Vector3 medidasDetector;

    [SerializeField] Transform detector;

    // Start is called before the first frame update
    void Start()
    {
        anima = GetComponent<Animator>();
        abierto = false;
        coliderPuerta = puertas.GetComponent<Collider>();
        coliders = GetComponents<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        coliders = Physics.OverlapBox(detector.position, medidasDetector, detector.rotation, capaJugador);

        if (coliders.Length > 0 && Input.GetButtonDown("Interaccion"))
        {
                Interact();
        }

        if (abierto)
        {
            anima.SetBool("abrase", true);
            coliderPuerta.enabled = false;
        }
        else
        {
            anima.SetBool("abrase", false);
            coliderPuerta.enabled = true;
        }
    }

    public override void Interact()
    {
        base.Interact();

        abierto = !abierto;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detector.position, medidasDetector);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCubo : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 10f;
    public float agacharseAltura = 0.5f;

    private bool agachado = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movimiento horizontal
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical) * velocidadMovimiento;

        // Salto
        if (Input.GetButtonDown("Jump") && !agachado)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        // Agacharse
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!agachado)
            {
                transform.localScale = new Vector3(1, agacharseAltura, 1);
                agachado = true;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                agachado = false;
            }
        }

        rb.velocity = movimiento;
    }
}

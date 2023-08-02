using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDeBotones : MonoBehaviour
{
    private int botonesPresionados = 0;

    public Animator Puerta;

    // En este ejemplo, asumiremos que todos los botones están en la escena y tienen el tag "Boton"
    private void Start()
    {
        // Obtener la referencia al Animator de la puerta
        Puerta.SetBool("Aparecer", false);
    }

    public void BotonPresionado()
    {
        botonesPresionados++;

        if (botonesPresionados >= 4)
        {
            // Si todos los botones están presionados, activar la puerta
            Puerta.SetBool("Aparecer", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public Animator Botoncio; // Las animaciones del botón y la puerta
    public Animator Puerta;

    private ControlDeBotones controlDeBotones;

    void Start()
    {
        Botoncio.SetBool("On", false);
        Puerta.SetBool("Aparecer", false);

        // Obtener la referencia al script ControlDeBotones
        controlDeBotones = FindObjectOfType<ControlDeBotones>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Botoncio.SetBool("On", true);

        // Notificar al ControlDeBotones que este botón fue presionado
        controlDeBotones.BotonPresionado();
    }
}

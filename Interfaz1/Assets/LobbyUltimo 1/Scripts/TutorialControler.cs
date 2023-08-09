using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControler : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] FirstPersonLook _jugador;

    [SerializeField] Image Estamina;
    [SerializeField] Image Cansancio;
    [SerializeField] Image botonInteraccion;

    private void Start()
    {
        _jugador.enabled = false;

        Estamina.enabled = false;
        Cansancio.enabled = false;
        botonInteraccion.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Tutorial()
    {
        Destroy(tutorial);
        _jugador.enabled = true;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}

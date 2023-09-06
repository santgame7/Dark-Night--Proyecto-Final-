using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControler : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject insTutorial;
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
        insTutorial.SetActive(false);
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Destroy(tutorial);
            insTutorial.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }
    public void Tutorial()
    {
        Destroy(tutorial);
        _jugador.enabled = true;
        insTutorial.SetActive(true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}

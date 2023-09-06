using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Codigo_Pausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    [SerializeField] FirstPersonLook _jugador;
    public bool Pausa = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Pausa == false)
            {
                ObjetoMenuPausa.SetActive(true);
                Pausa = true;

                Time.timeScale = 0;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

                _jugador.enabled = false;

                for (int i = 0; i < sonidos.Length; i++)
                {
                    sonidos[i].Pause();
                }
            }
            else if (Pausa == true)
            {
                Resumir();
            }
        }

    }

    public void Resumir()
    {
        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        AudioSource[] sonidos = FindObjectsOfType<AudioSource>();

        _jugador.enabled = true;

        for (int i = 0; i < sonidos.Length; i++)
        {
            sonidos[i].Play();
        }
    }
    
    public void IrMenu(string NombreMenu)
    {
        SceneManager.LoadScene(NombreMenu);
        Time.timeScale = 1;
    }
}

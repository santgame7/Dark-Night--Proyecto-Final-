using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    public void Jugar(string Nivel)
    {
        SceneManager.LoadScene(Nivel);
    }
    // Start is called before the first frame update
    public void Salir()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Empezar(string Lobby)
    {
        SceneManager.LoadScene(Lobby);
    }
    public void Salir()
    {
        Application.Quit();
    }
}

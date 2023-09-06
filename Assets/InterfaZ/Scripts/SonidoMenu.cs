using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoMenu : MonoBehaviour
{
    public AudioSource sonido;
    public AudioClip sonidoMenu;
    public void SonidoBoton()
    {
        sonido.clip = sonidoMenu;

        sonido.enabled = false;
        sonido.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarNivel_1 : MonoBehaviour
{
    public GameObject PantallaDeCarga;
    public Slider Slider;

      
    public void CargarScene(int NumeroDeEscena)
    {
        StartCoroutine(CargarAsync(NumeroDeEscena));
    }

    IEnumerator CargarAsync(int NumeroDeEscena)
    {
        AsyncOperation Operacion = SceneManager.LoadSceneAsync(NumeroDeEscena);

        PantallaDeCarga.SetActive(true);

        while (!Operacion.isDone)
        {
            float Progreso = Mathf.Clamp01(Operacion.progress / .9f);
            Slider.value = Progreso;

            yield return null;
        }
    }

}

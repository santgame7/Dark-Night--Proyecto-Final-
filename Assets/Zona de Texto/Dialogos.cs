using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dialogos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtmp;
    [SerializeField] string texto;
    private bool textoMostrado = false; // Para controlar si el texto ya ha sido mostrado

<<<<<<< Updated upstream
=======
    [SerializeField] Animator aniPuerta;

>>>>>>> Stashed changes
    private void Start()
    {
        txtmp.text = ""; // Borra el texto después de 60 segundos
    }
<<<<<<< Updated upstream
=======

    

>>>>>>> Stashed changes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !textoMostrado) // Cambia "Jugador" por la etiqueta correcta del objeto que activará el trigger
        {
            StartCoroutine(MostrarTextoPorDuracion());
            textoMostrado = true; // Evita mostrar el texto nuevamente en futuras colisiones
        }
        if (other.CompareTag("Player"))
        {
            aniPuerta.SetBool("Abrir", false); //se cierra sola la puerta
            StartCoroutine(DesactivarAnimator()); // desactiva el animator, para que no se vuelva a abrir.
        }
    }

    private IEnumerator DesactivarAnimator()
    {
        yield return new WaitForSeconds(1.5f); // Ajusta el tiempo de retraso según sea necesario
        aniPuerta.enabled = false; // Desactiva el componente Animator
    }

    private IEnumerator MostrarTextoPorDuracion()
    {
        txtmp.text = texto; // Reemplaza con el texto que quieras mostrar

        yield return new WaitForSeconds(10.0f); // Espera durante 60 segundos

        txtmp.text = ""; // Borra el texto después de 60 segundos
    }
}

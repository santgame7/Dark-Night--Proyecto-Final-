using UnityEngine;
using UnityEngine.SceneManagement;

public class llave1 : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0); // Cargar la Scene n�mero 1 al tocar al jugador.
        }
    }
}

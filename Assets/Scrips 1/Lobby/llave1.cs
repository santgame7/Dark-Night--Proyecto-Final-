using UnityEngine;
using UnityEngine.SceneManagement;

public class llave1 : MonoBehaviour
{
    public int scena;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(scena); // Cargar la Scene n�mero 1 al tocar al jugador.
        }
    }
}

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad = 5f;
    public string tagObjetivo = "Player";

    private void Update()
    {
        // Obtener la dirección hacia el objetivo
        Vector3 direccion = objetivo.position - transform.position;
        direccion.Normalize();

        // Mover el objeto hacia el objetivo
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(3); // Cargar la Scene número 3 al colisionar con el jugador.
        }
    }
}
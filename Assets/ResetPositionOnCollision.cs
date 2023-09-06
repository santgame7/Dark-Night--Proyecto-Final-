using UnityEngine;

public class ResetPositionOnCollision : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posición inicial del objeto.
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión fue con un objeto que tiene el tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restablece la posición del objeto a la posición inicial.
            transform.position = initialPosition;
        }
    }
}

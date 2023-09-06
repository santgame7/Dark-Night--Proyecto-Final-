using UnityEngine;

public class ResetPositionOnCollision : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posici�n inicial del objeto.
        initialPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n fue con un objeto que tiene el tag "Player".
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restablece la posici�n del objeto a la posici�n inicial.
            transform.position = initialPosition;
        }
    }
}

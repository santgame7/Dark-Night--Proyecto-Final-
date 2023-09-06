using UnityEngine;

public class TeleportOnCollision : MonoBehaviour
{
    public Transform teleportDestination; // El punto de destino al que se teletransportará el jugador.

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player".
        if (other.CompareTag("Player"))
        {
            // Teletransporta al jugador al punto de destino.
            other.transform.position = teleportDestination.position;
        }
    }
}

using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public string pickableTag = "Pickable";
    public float moveDistance = 2.0f;
    public float moveSpeed = 2.0f;
    public GameObject objectToMove;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool movingUp = false; // Cambiado a falso por defecto

    private void Start()
    {
        initialPosition = objectToMove.transform.position;
        targetPosition = initialPosition + Vector3.up * moveDistance;
    }

    private void Update()
    {
        if (movingUp)
        {
            MoveObject(targetPosition);
            if (objectToMove.transform.position == targetPosition)
                movingUp = false;
        }
        else
        {
            MoveObject(initialPosition);
            if (objectToMove.transform.position == initialPosition)
                movingUp = true;
        }
    }

    private void MoveObject(Vector3 target)
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, target, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(pickableTag))
        {
            other.transform.parent = objectToMove.transform;
            movingUp = true; // Activar el movimiento cuando colisiona con un objeto "Pickable"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(pickableTag))
        {
            other.transform.parent = null;
            movingUp = false; // Desactivar el movimiento al salir de la colisión con un objeto "Pickable"
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableObject2 : MonoBehaviour
{
    public GameObject objectA; // Primer objeto a verificar
    public GameObject objectB; // Segundo objeto a verificar
    public string pickableTag = "Pickable";
    public GameObject objectToMove; // Objeto a mover hacia arriba
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f;
    public AudioClip soundEffect;
    private bool isMoving = false;

    private void Update()
    {
        // Verifica si ambos objetos A y B están colisionando con un objeto Pickable al mismo tiempo
        bool isBothColliding = IsObjectCollidingWithPickable(objectA) && IsObjectCollidingWithPickable(objectB);

        if (isBothColliding && !isMoving)
        {
            Vector3 targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            StartCoroutine(MoveObject(targetPosition));
        }
        else if (!isBothColliding && isMoving)
        {
            Vector3 targetPosition = objectToMove.transform.position - Vector3.up * moveDistance;
            StartCoroutine(MoveObject(targetPosition));
        }
    }

    private bool IsObjectCollidingWithPickable(GameObject obj)
    {
        if (obj == null)
        {
            return false;
        }

        Collider[] colliders = Physics.OverlapBox(obj.transform.position, obj.transform.localScale / 2, Quaternion.identity);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(pickableTag))
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator MoveObject(Vector3 targetPosition)
    {
        Vector3 startPosition = objectToMove.transform.position;
        float elapsedTime = 0;

        while (elapsedTime < moveSpeed)
        {
            objectToMove.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = targetPosition;

        if (soundEffect != null)
        {
            AudioSource.PlayClipAtPoint(soundEffect, objectToMove.transform.position);
        }

        isMoving = !isMoving; // Toggle the moving state
    }
}

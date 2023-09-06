using UnityEngine;
using System.Collections;


public class PickableObject : MonoBehaviour
{
    public string pickableTag = "Pickable";
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f;
    public GameObject objectToMove;
    public AudioClip soundEffect;
    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(pickableTag) && !isMoving)
        {
            Vector3 targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            StartCoroutine(MoveObject(targetPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(pickableTag) && isMoving)
        {
            Vector3 targetPosition = objectToMove.transform.position - Vector3.up * moveDistance;
            StartCoroutine(MoveObject(targetPosition));
        }
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

using UnityEngine;

public class RaycastPickup : MonoBehaviour
{
    public float raycastDistance = 5f; // Distancia del rayo

    private GameObject pickedObject = null;
    private float pickupDistance = 0f; // Distancia entre el objeto y el personaje al recogerlo

    void Update()
    {
        // Lanzar el rayo permanentemente en la dirección hacia adelante
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.green);

        if (pickedObject == null)
        {
            // Si no hay objeto recogido, detectar si hay un objeto "Pickable"
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Si el objeto es "Pickable", lo recogemos
                    if (hit.collider.CompareTag("Pickable"))
                    {
                        pickedObject = hit.collider.gameObject;
                        pickupDistance = Vector3.Distance(transform.position, pickedObject.transform.position);
                    }
                }
            }
        }
        else
        {
            // Si hay objeto recogido, moverlo hacia la posición del cursor del mouse
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = pickupDistance; // Ajustar la posición z para mantener la misma distancia de recogida

            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            pickedObject.transform.position = worldMousePosition;
        }

        // Soltar el objeto cuando se suelte la tecla "E"
        if (Input.GetKeyUp(KeyCode.E))
        {
            pickedObject = null;
        }
    }
}


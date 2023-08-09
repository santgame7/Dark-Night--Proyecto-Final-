using UnityEngine;
using UnityEngine.UI;

public class ObjectPicker : MonoBehaviour
{
    public float pickupDistance = 2f;
    public KeyCode pickupKey = KeyCode.E;
    public GameObject linterna;
    public Image interactionImage;
    public Animator ani;// Agrega la referencia a la imagen en el Inspector

    private Transform pickedObject;
    private Camera mainCamera;
    private bool isHolding = false;

    private void Start()
    {
        mainCamera = Camera.main;
        interactionImage.gameObject.SetActive(false); // Asegurarse de que la imagen esté desactivada al inicio
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        // Realizar el raycast
        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            // Verificar si el raycast toca un objeto "Pickable" o "Linterna"
            if (hit.collider.CompareTag("Pickable") || hit.collider.CompareTag("Linterna") || hit.collider.CompareTag("Libro") || hit.collider.CompareTag("Puerta"))
            {
                interactionImage.gameObject.SetActive(true); // Mostrar la imagen
            }
            else
            {
                interactionImage.gameObject.SetActive(false); // Ocultar la imagen si no hay objeto válido
            }

            // Resto del código...
            if (pickedObject == null)
            {
                if (Input.GetKeyDown(pickupKey))
                {
                    TryPickObject();
                }
            }
            else
            {
                if (Input.GetKeyDown(pickupKey))
                {
                    TryDropObject();
                }
                else
                {
                    MovePickedObject();
                }
            }
        }
        else
        {
            interactionImage.gameObject.SetActive(false); // Ocultar la imagen si el raycast no toca nada

            // Resto del código...
            if (pickedObject != null)
            {
                if (Input.GetKeyDown(pickupKey))
                {
                    TryDropObject();
                }
                else
                {
                    MovePickedObject();
                }
            }
        }
    }

    private void TryPickObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                pickedObject = hit.transform;
                isHolding = true;
            }
            else if (hit.collider.CompareTag("Linterna") && !isHolding)
            {
                // Destroy the "linterna" object and activate a new object
                Destroy(hit.collider.gameObject);
                ActivateNewObject();
            }
            if (hit.collider.CompareTag("Puerta"))
            {
                
            }
        }
    }

    private void MovePickedObject()
    {
        Vector3 newPosition = mainCamera.transform.position + mainCamera.transform.forward * pickupDistance;
        pickedObject.position = newPosition;
    }

    private void TryDropObject()
    {
        ChangeColorOnCollision changeColorScript = pickedObject.GetComponent<ChangeColorOnCollision>();
        if (changeColorScript && (!changeColorScript.IsRed() || !IsCollidingWithRedObjects()))
        {
            DropObject();
            Rigidbody pickedObjectRigidbody = pickedObject.GetComponent<Rigidbody>();
            if (pickedObjectRigidbody != null)
            {
                pickedObjectRigidbody.velocity = Vector3.zero; // Detener la velocidad del objeto al soltarlo
                pickedObjectRigidbody.angularVelocity = Vector3.zero; // Detener la velocidad angular del objeto al soltarlo
            }
        }
    }

    private void DropObject()
    {
        pickedObject = null;
        isHolding = false;
    }

    private bool IsCollidingWithRedObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(pickedObject.position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Cosa"))
            {
                return true;
            }
        }
        return false;
    }

    private void ActivateNewObject()
    {
        linterna.SetActive(true);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ObjectPicker : MonoBehaviour
{
    public float pickupDistance = 2f;
    public KeyCode pickupKey = KeyCode.E;
    private GameObject linterna;
    public Image interactionImage;
    private GameObject puerta;

    private Transform pickedObject;
    private Camera mainCamera;
    private bool isHolding = false;

    private void Start()
    {
        mainCamera = Camera.main;
        interactionImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance))
        {
            if (hit.collider.CompareTag("Pickable") || hit.collider.CompareTag("Linterna") || hit.collider.CompareTag("Libro") || hit.collider.CompareTag("Puerta"))
            {
                interactionImage.gameObject.SetActive(true);
            }
            else
            {
                interactionImage.gameObject.SetActive(false);
            }

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
            interactionImage.gameObject.SetActive(false);

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
                Destroy(hit.collider.gameObject);
                ActivateNewObject();
            }
            if (hit.collider.CompareTag("Puerta"))
            {
                TryToggleDoor(hit.collider.gameObject);
            }
        }
    }

    private void TryToggleDoor(GameObject door)
    {
        Animator doorAnimator = door.GetComponent<Animator>();
        if (doorAnimator != null)
        {
            doorAnimator.SetBool("Abrir", !doorAnimator.GetBool("Abrir"));
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
                pickedObjectRigidbody.velocity = Vector3.zero;
                pickedObjectRigidbody.angularVelocity = Vector3.zero;
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
            if (collider.CompareTag("Cosa") || collider.gameObject.CompareTag("Pickable") || collider.gameObject.CompareTag("Suelo"))
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
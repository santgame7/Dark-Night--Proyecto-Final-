using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del personaje
    public float rotationSpeed = 100f; // Velocidad de rotaci�n del personaje
    public Transform cameraTransform; // Referencia al objeto Transform de la c�mara
    public float cameraRotationSpeed = 2f; // Velocidad de rotaci�n de la c�mara

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Para evitar rotaciones f�sicas no deseadas
    }

    void Update()
    {
        // Movimiento del personaje
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        // Rotaci�n del personaje
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }

        // Rotaci�n de la c�mara
        float mouseX = Input.GetAxis("Mouse X") * cameraRotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * cameraRotationSpeed;
        Vector3 rotation = cameraTransform.rotation.eulerAngles;
        rotation.x -= mouseY;
        rotation.y += mouseX;
        cameraTransform.rotation = Quaternion.Euler(rotation);
    }
}

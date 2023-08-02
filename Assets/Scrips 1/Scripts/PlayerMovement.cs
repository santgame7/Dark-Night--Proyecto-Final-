using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private float distanceToGround = 0.2f;

    [SerializeField] private float speed;
    [SerializeField] private float velocity=300;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private Vector2 sensibility;
   [SerializeField] private new Transform camera;

    [SerializeField] private float rayDistance;

    [SerializeField] Image barraEstamina;
    [SerializeField] Image barraCansancio;
    [SerializeField] Image interaccionBoton;

    [SerializeField] float estamina;
    [SerializeField] float estaminaMaxima;

    bool cansancio = false;

    // Variable para controlar si el jugador está en el suelo
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        //camera = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;

        speed = velocity;

        barraCansancio.enabled = false;
        barraEstamina.enabled = false;
        interaccionBoton.enabled = false;
    }

    private void UpdateMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity;

        if (hor != 0 || ver != 0)
        {
            velocity = (transform.forward * ver + transform.right * hor).normalized * speed * Time.deltaTime;
        }
        else
        {
            velocity = Vector3.zero;
        }

        velocity.y = Rigidbody.velocity.y;
        Rigidbody.velocity = velocity;
    }

    private bool IsGrounded()
    {
        return Physics.BoxCast(transform.position, new Vector3(0.4f, 0f, 0.4f), Vector3.down, Quaternion.identity, distanceToGround + 0.1f);
    }

    private void UpdateJump()
    {
        // Verificar si se presionó el botón de salto y si el jugador está en el suelo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Agregar una fuerza hacia arriba para el salto
            Rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

            // Establecer que el jugador no está en el suelo para evitar saltos múltiples
            isGrounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateJump();

        RaycastHit hit;

        if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, LayerMask.GetMask("Interactuar")))
        {
            interaccionBoton.enabled = true;
        }
        else
        {
            interaccionBoton.enabled = false;
        }

        if (Input.GetButtonDown("Interaccion"))
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, rayDistance, LayerMask.GetMask("Interactuar")))
            {
                hit.transform.GetComponent<Interactuable>().Interact();
            }
        }

        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibility.x);
        }

        if (ver != 0)
        {
            float angle = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;

            if (angle > 180)
            {
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, -75, 75);

            camera.localEulerAngles = Vector3.right * angle;
        }

        barraEstamina.fillAmount = estamina / estaminaMaxima;

        if (Input.GetKey(KeyCode.W) && Input.GetAxis("Correr") != 0 && !cansancio)
        {
            speed = runSpeed;
            estamina -= 33f * Time.deltaTime;
            barraCansancio.enabled = true;
            barraEstamina.enabled = true;
        }
        else
        {
            speed = velocity;
            if (estamina <= estaminaMaxima - 0.01)
            {
                estamina += 15f * Time.deltaTime;
            }
        }

        if (estamina <= 0)
        {
            cansancio = true;
            barraCansancio.enabled = false;
        }

        if (estamina >= estaminaMaxima || estamina >= estaminaMaxima - 0.1)
        {
            cansancio = false;
            barraCansancio.enabled = false;
            barraEstamina.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(camera.position, camera.forward * rayDistance);
    }

    // Colisión con el suelo u otro objeto
    private void OnCollisionEnter(Collision collision)
    {
        // Si el jugador colisiona con el suelo, establecer que está en el suelo
        if (collision.gameObject.CompareTag("Untagged"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Pickable"))
        {
            isGrounded = true;
        }
    }
}

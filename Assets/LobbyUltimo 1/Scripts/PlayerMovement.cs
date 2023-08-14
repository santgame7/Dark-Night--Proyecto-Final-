using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Basico 
    private Rigidbody Rigidbody;
    [SerializeField] private float distanceToGround;

    
    [Header("Movimiento Basico")]
    [SerializeField] private float velocity;
    private float speed;
    [SerializeField] private float pique;
    [SerializeField] private float jumpForce;

    [Header("Movimiento Camara")]
    [SerializeField] private Vector2 sensibility;
    private new Transform camera;

    [Header("Distancia del rasho")]
    [SerializeField] private float rayDistance;
    #endregion

    #region Canvas
    [Header("UI")]
    [SerializeField] Image barraEstamina;
    [SerializeField] Image barraCansancio;
    [SerializeField] Image interaccionBoton;
    [SerializeField] Image nivelBateria; //esto es para asignar la barra de energia que va a bajar
    [SerializeField] Image barraBateria; 
    #endregion

    #region Estamina
    [Header("Sistema estamina")]
    [SerializeField] float estamina;
    [SerializeField] float estaminaMaxima;

    bool cansancio = false;
    #endregion

    #region Linterna
    [Header("Linterna")]
    public Light LuzLinterna;        // la luz de la linterna osea , el spot light

    [Header("Energia")]              //el header es un baino para agrupar todo lo que tenga que ver con la energia
    public float EnergiaActual = 100;
    public float EnergiaMaxima = 100;
    public float VelocidadConsumo = 0.5f;
    public float VelocidadDeRecarga = 0.2f;
    #endregion

    #region Validaciones
    bool agarroLinterna;
    #endregion

    public bool AgarroLinterna { get => agarroLinterna; set => agarroLinterna = value; }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        camera = GetComponentInChildren<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;

        speed = velocity;

        barraCansancio.enabled = false;
        barraEstamina.enabled = false;
        interaccionBoton.enabled = false;

        nivelBateria.enabled = false;
        barraBateria.enabled = false;

        AgarroLinterna = false;
    }

    private void UpdateMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 velocity;

        if (hor != 0 | ver != 0)
        {
            velocity = (transform.forward * ver + transform.right * hor).normalized * speed;

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
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateJump();
        SistemaEstamina();

        if (AgarroLinterna)
        {
            SistemaLinterna();
            nivelBateria.enabled = true;
            barraBateria.enabled = true;
        }

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
            //camera.Rotate(Vector3.left * ver * sensibility.y);
            float angle = (camera.localEulerAngles.x - ver * sensibility.y + 360) % 360;

            if (angle > 180)
            {
                angle -= 360;
            }
            angle = Mathf.Clamp(angle, -75, 75);

            camera.localEulerAngles = Vector3.right * angle;
        }

        
    }

    private void SistemaEstamina()
    {
        barraEstamina.fillAmount = estamina / estaminaMaxima;

        if (Input.GetAxis("Correr") != 0 && !cansancio)
        {
            speed = velocity + pique;
            estamina -= 15f * Time.deltaTime;
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

    private void SistemaLinterna()
    {
        //aqui se prende y se apaga la linterna , coge un axis llamado linterna que tiene la letra f asignada
        if (Input.GetButtonDown("FlashLight"))
        {
            if (LuzLinterna.enabled == true)
            {
                LuzLinterna.enabled = false;
            }
            else if (LuzLinterna.enabled == false && EnergiaActual > 10)
            {
                LuzLinterna.enabled = true;
            }
        }

        if (LuzLinterna.enabled == true) //esto es de que si llega a 0 la bateria , se apaga la linterna
        {
            EnergiaActual -= Time.deltaTime * VelocidadConsumo;

            if (EnergiaActual <= 0)
            {
                EnergiaActual = 0;
                LuzLinterna.enabled = false;
            }
        }
        else if (LuzLinterna.enabled == false)
        {
            EnergiaActual += Time.deltaTime * VelocidadDeRecarga;

            if (EnergiaActual > EnergiaMaxima)
            {
                EnergiaActual = EnergiaMaxima;
            }

        }

        nivelBateria.fillAmount = EnergiaActual / EnergiaMaxima; //el codigo para que baje la barra
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(camera.position, camera.forward * rayDistance);
    }
}
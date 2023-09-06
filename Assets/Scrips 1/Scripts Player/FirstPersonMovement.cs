using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Stamina")]
    public Slider staminaSlider;
    public float maxStamina = 100;
    public float currentStamina;
    public float staminaRecoveryRate = 10;

     new Rigidbody rigidbody;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
    }

    void FixedUpdate()
    {
        IsRunning = canRun && Input.GetKey(runningKey);

        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);

        // Decrementa la estamina cuando se está corriendo.
        if (IsRunning)
        {
            currentStamina -= Time.deltaTime * (IsRunning ? 1 : staminaRecoveryRate);
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }
        else
        {
            // Incrementa la estamina cuando no se está corriendo.
            currentStamina += Time.deltaTime * staminaRecoveryRate;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        }

        // Actualiza el valor de la barra de estamina.
        staminaSlider.value = currentStamina / maxStamina;
    }
}

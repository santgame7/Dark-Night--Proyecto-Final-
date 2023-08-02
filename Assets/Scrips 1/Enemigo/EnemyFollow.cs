using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    public Transform targetTransform; // Objetivo a seguir
    public float speed = 5f; // Velocidad de movimiento del enemigo
    public float chaseDuration = 10f; // Duración de la persecución en segundos
    public float restDuration = 20f; // Duración del descanso en segundos antes de reanudar la persecución
    public float stoppingDistance = 1f; // Distancia mínima a la que el enemigo considera que ha alcanzado al objetivo
    public float raycastDistance = 2f; // Distancia del Raycast hacia abajo para encontrar el suelo
    public float sideRaycastDistance = 1f; // Distancia de los Raycast laterales para detectar obstáculos
    public LayerMask groundLayer; // Capa que representa el terreno

    public float randomMoveRadiusMin = 5f; // Radio mínimo para el movimiento aleatorio durante el descanso
    public float randomMoveRadiusMax = 15f; // Radio máximo para el movimiento aleatorio durante el descanso

    private Rigidbody rb; // Referencia al Rigidbody del enemigo para moverlo
    private bool isChasing = true; // Indica si el enemigo está persiguiendo al objetivo actualmente
    private float chaseTimer = 0f; // Contador para la duración de la persecución o descanso

    private bool isTurning = false; // Indica si el enemigo está girando en una nueva dirección durante el descanso
    private Vector3 randomDirection; // Dirección aleatoria hacia la cual el enemigo se girará

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
       // rb.useGravity = false; // Deshabilitar la gravedad para que el enemigo no caiga

        // Asegurar que el movimiento solo esté restringido en el plano XZ
       // rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    private void Update()
    {
        if (targetTransform == null)
            return;

        if (isChasing)
        {
            ChaseTarget();
        }
        else
        {
            Rest();
        }
    }

    private void ChaseTarget()
    {
        Vector3 direction = (targetTransform.position - transform.position).normalized;
        rb.velocity = direction * speed * Time.deltaTime;

        if (rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

        // Si el enemigo está lo suficientemente cerca del objetivo, se detiene y cambia a la fase de descanso
        if (distanceToTarget < stoppingDistance)
        {
            isChasing = false;
            chaseTimer = 0f;
            rb.velocity = Vector3.zero;
        }
        else
        {
            // Incrementamos el temporizador de persecución
            chaseTimer += Time.deltaTime;

            // Si ha pasado el tiempo de persecución, cambia a la fase de descanso
            if (chaseTimer >= chaseDuration)
            {
                isChasing = false;
                chaseTimer = 0f;
                rb.velocity = Vector3.zero;
            }
        }

        // Asegurarse de que el enemigo esté en el suelo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            transform.position = hit.point;
        }

        // Detectar y esquivar objetos en los lados
        AvoidObstacles();
    }

    private void AvoidObstacles()
    {
        // Raycast hacia la derecha
        RaycastHit rightHit;
        if (Physics.Raycast(transform.position, transform.right, out rightHit, sideRaycastDistance))
        {
            // Si detecta un obstáculo a la derecha, moverse hacia la izquierda para esquivarlo
            rb.velocity -= transform.right * speed * Time.deltaTime;
        }

        // Raycast hacia la izquierda
        RaycastHit leftHit;
        if (Physics.Raycast(transform.position, -transform.right, out leftHit, sideRaycastDistance))
        {
            // Si detecta un obstáculo a la izquierda, moverse hacia la derecha para esquivarlo
            rb.velocity += transform.right * speed * Time.deltaTime;
        }
    }

    private void Rest()
    {
        // Incrementamos el temporizador de descanso
        chaseTimer += Time.deltaTime;

        // Si ha pasado el tiempo de descanso, cambia a la fase de persecución
        if (chaseTimer >= restDuration)
        {
            isChasing = true;
            chaseTimer = 0f;
            isTurning = false;
            return;
        }

        // Si estamos en el descanso y no hemos iniciado la rotación aleatoria
        if (!isChasing && !isTurning)
        {
            isTurning = true;
            // Generar una dirección aleatoria para la rotación durante el descanso
            randomDirection = Random.onUnitSphere;
        }

        // Si estamos en el descanso y hemos iniciado la rotación aleatoria
        if (isTurning)
        {
            // Rotar gradualmente hacia la nueva dirección
            Quaternion targetRotation = Quaternion.LookRotation(randomDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 60f * Time.deltaTime);

            // Si casi hemos alcanzado la dirección aleatoria, dejamos de girar y avanzamos en esa dirección
            if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
            {
                isTurning = false;
                rb.velocity = randomDirection * speed * Time.deltaTime;
            }
        }

        // Asegurarse de que el enemigo esté en el suelo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer))
        {
            transform.position = hit.point;
        }

        // Detectar y esquivar objetos en los lados
        AvoidObstacles();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Colisionó con el jugador, cargar la escena 3
            SceneManager.LoadScene("NombreDeTuEscena3");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sideRaycastDistance);
    }
}

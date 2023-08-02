using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 50f;
    public float closeDistance = 10f;
    public float speedWhenFar = 3f;
    public float speedWhenClose = 6f;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > chaseDistance)
        {
            agent.speed = speedWhenFar;
            agent.SetDestination(player.position);
        }
        else if (distanceToPlayer <= closeDistance)
        {
            agent.speed = speedWhenClose;
            agent.SetDestination(player.position);
        }
        else
        {
            // If the player is between chaseDistance and closeDistance, stop moving.
            agent.SetDestination(transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(3); // Cargar la Scene número 3 al colisionar con el jugador.
        }
    }
}


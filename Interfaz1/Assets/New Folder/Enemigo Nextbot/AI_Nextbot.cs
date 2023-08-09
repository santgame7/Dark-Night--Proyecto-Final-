using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Nextbot : MonoBehaviour
{
    public Transform objetivo;
    public float velocidadInicial = 2f;
    public float aumentoVelocidad = 0.3f;
    public float intervaloAumentoVelocidad = 10f;
    public float distanciaMaximaSonido = 5f;
    public AudioClip sonidoAsecho;
    public float volumenSonidoAsecho = 0.5f;

    private NavMeshAgent IA;
    private AudioSource audioSource;

    void Start()
    {
        IA = GetComponent<NavMeshAgent>();
        IA.speed = velocidadInicial;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = sonidoAsecho;
        audioSource.volume = 0f;

        // Iniciar la rutina para aumentar la velocidad gradualmente
        StartCoroutine(AumentarVelocidadConTiempo());
    }

    void Update()
    {
        if (objetivo == null) return;

        // Calcular la dirección hacia el objetivo sin tomar en cuenta la altura
        Vector3 direccionAlObjetivo = objetivo.position - transform.position;
        direccionAlObjetivo.y = 0f;

        // Rotar enemigo para mirar en la dirección opuesta al jugador
        transform.rotation = Quaternion.LookRotation(-direccionAlObjetivo, Vector3.up);

        // Solo seguir al objetivo si está a una distancia mayor que 2 unidades
        if (direccionAlObjetivo.magnitude > 2f)
        {
            IA.SetDestination(objetivo.position);
        }

        // Calcular la distancia al jugador
        float distanciaAlObjetivo = direccionAlObjetivo.magnitude;

        // Ajustar el volumen del sonido según la distancia al jugador
        audioSource.volume = Mathf.Clamp01(1f - (distanciaAlObjetivo / distanciaMaximaSonido));

        // Reproducir el sonido de acecho si el enemigo está cerca del jugador
        if (distanciaAlObjetivo <= distanciaMaximaSonido)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    // Rutina para aumentar la velocidad gradualmente cada intervalo de tiempo
    IEnumerator AumentarVelocidadConTiempo()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloAumentoVelocidad);
            velocidadInicial += aumentoVelocidad;
            IA.speed = velocidadInicial;
        }
    }
}
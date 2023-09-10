using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform puntoSuperior; // Punto vac�o en la parte m�s alta
    public Transform puntoInferior; // Punto vac�o en la parte m�s baja
    public float velocidad = 2.0f; // Velocidad de movimiento de la plataforma

<<<<<<< Updated upstream
    private bool moviendoseHaciaArriba = false;

    private void Start()
    {
        // Inicialmente, la plataforma se encuentra en la parte inferior
        transform.position = puntoInferior.position;
    }
=======
    private bool moviendoseHaciaArriba = true;
>>>>>>> Stashed changes

    private void Update()
    {
        // Calcula la direcci�n del movimiento
<<<<<<< Updated upstream
        Vector3 direccion = (moviendoseHaciaArriba) ? puntoSuperior.position - transform.position : puntoInferior.position - transform.position;

        // Normaliza la direcci�n para mantener una velocidad constante
        direccion.Normalize();
=======
        Vector3 direccion = (moviendoseHaciaArriba) ? Vector3.up : Vector3.down;
>>>>>>> Stashed changes

        // Mueve la plataforma en la direcci�n calculada y a la velocidad especificada
        transform.Translate(direccion * velocidad * Time.deltaTime);

<<<<<<< Updated upstream
        // Comprueba si la plataforma ha llegado a uno de los puntos vac�os
        if (moviendoseHaciaArriba && Vector3.Distance(transform.position, puntoSuperior.position) < 0.01f)
        {
            moviendoseHaciaArriba = false;
        }
        else if (!moviendoseHaciaArriba && Vector3.Distance(transform.position, puntoInferior.position) < 0.01f)
=======
        // Comprueba si la plataforma ha llegado al punto superior o inferior
        if (moviendoseHaciaArriba && transform.position.y >= puntoSuperior.position.y)
        {
            moviendoseHaciaArriba = false;
        }
        else if (!moviendoseHaciaArriba && transform.position.y <= puntoInferior.position.y)
>>>>>>> Stashed changes
        {
            moviendoseHaciaArriba = true;
        }
    }
}

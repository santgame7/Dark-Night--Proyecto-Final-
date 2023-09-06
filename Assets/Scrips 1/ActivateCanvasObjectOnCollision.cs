using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateCanvasObjectOnCollision : MonoBehaviour
{
    public GameObject canvasObjectToActivate; // El objeto del Canvas que deseas activar.
    public AudioClip collisionSound; // El sonido a reproducir al colisionar con el objeto.

    private bool hasCollided = false;
    private AudioSource audioSource;

    void Start()
    {
        // Obtén la referencia al componente AudioSource si el sonido está configurado.
        if (collisionSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = collisionSound;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player" y aún no ha colisionado antes.
        if (other.CompareTag("Player") && !hasCollided)
        {
            // Activa el objeto en el Canvas.
            if (canvasObjectToActivate != null)
            {
                canvasObjectToActivate.SetActive(true);

                // Inicia la corutina para desvanecer gradualmente la imagen después de 2 segundos.
                StartCoroutine(FadeOutImage(2f));
            }

            // Reproduce el sonido si está configurado.
            if (audioSource != null && collisionSound != null)
            {
                audioSource.Play();
            }

            // Marca que ha ocurrido una colisión.
            hasCollided = true;
        }
    }

    // Corutina para desvanecer gradualmente la imagen.
    IEnumerator FadeOutImage(float fadeDuration)
    {
        Image image = canvasObjectToActivate.GetComponent<Image>();
        if (image != null)
        {
            Color startColor = image.color;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Desactiva el objeto en el Canvas después de desvanecerlo.
            canvasObjectToActivate.SetActive(false);
        }
    }
}

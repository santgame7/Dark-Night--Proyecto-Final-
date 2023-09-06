using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetectorLibros : MonoBehaviour
{
    public int contadorLibros = 0;
    public int contadorObjetivo = 5;
    public GameObject Llave;

    public TextMeshProUGUI contadorTexto;

    public Image[] imagenesLibros; // Grupo de imágenes de libros en el Inspector
    public KeyCode pickupKey = KeyCode.E;

    private bool hasInteracted = false;
    private int activeImageIndex = -1; // Índice de la imagen activa actual

    private void Start()
    {
        ActualizarContadorTexto();
        foreach (var imagen in imagenesLibros)
        {
            imagen.gameObject.SetActive(false); // Asegurarse de que las imágenes estén desactivadas al inicio
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (hasInteracted)
            {
                DesactivarImagenLibro();
                hasInteracted = false;
            }
            else
            {
                LanzarRayo();
            }
        }
    }

    private void LanzarRayo()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.forward, Camera.main.transform.position + Camera.main.transform.forward);

        float distancia;
        if (plane.Raycast(rayo, out distancia))
        {
            Vector3 puntoImpacto = rayo.GetPoint(distancia);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, puntoImpacto - Camera.main.transform.position, out hit, 1f))
            {
                if (hit.collider.CompareTag("Libro"))
                {
                    contadorLibros++;
                    Debug.Log("Libro recolectado. Contador: " + contadorLibros);

                    ActualizarContadorTexto();
                    MostrarImagenLibro(contadorLibros);

                    Destroy(hit.collider.gameObject);

                    if (contadorLibros >= contadorObjetivo)
                    {
                        Ganaste();
                    }
                }
            }
        }
    }

    private void ActualizarContadorTexto()
    {
        contadorTexto.text = contadorLibros + " / " + contadorObjetivo;
    }

    private void MostrarImagenLibro(int index)
    {
        if (index <= imagenesLibros.Length)
        {
            DesactivarImagenLibro();
            imagenesLibros[index - 1].gameObject.SetActive(true);
            activeImageIndex = index - 1;
            hasInteracted = true;
        }
    }

    private void DesactivarImagenLibro()
    {
        if (activeImageIndex >= 0 && activeImageIndex < imagenesLibros.Length)
        {
            imagenesLibros[activeImageIndex].gameObject.SetActive(false);
        }
    }

    private void Ganaste()
    {
        Debug.Log("¡Ganaste! Has recolectado 5 libros.");
        Llave.SetActive(true);
    }
}

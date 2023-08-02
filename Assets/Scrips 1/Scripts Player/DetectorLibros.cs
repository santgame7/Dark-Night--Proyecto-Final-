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

    private void Start()
    {
        ActualizarContadorTexto();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LanzarRayo();
        }
    }

    private void LanzarRayo()
    {
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Creamos un plano a 1 unidad de distancia de la posición de la cámara
        Plane plane = new Plane(Vector3.forward, Camera.main.transform.position + Camera.main.transform.forward);

        float distancia;
        if (plane.Raycast(rayo, out distancia))
        {
            // Obtenemos el punto de impacto en el plano
            Vector3 puntoImpacto = rayo.GetPoint(distancia);

            // Lanzamos un rayo desde la posición de la cámara al punto de impacto en el plano
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, puntoImpacto - Camera.main.transform.position, out hit, 1f))
            {
                if (hit.collider.CompareTag("libro"))
                {
                    contadorLibros++;
                    Debug.Log("Libro recolectado. Contador: " + contadorLibros);

                    ActualizarContadorTexto();

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

    private void Ganaste()
    {
        Debug.Log("¡Ganaste! Has recolectado 5 libros.");
        Llave.SetActive(true);

    }
}

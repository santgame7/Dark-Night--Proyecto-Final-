using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteraccionLibro : MonoBehaviour
{
    public Camera mainCamera; // La c�mara desde la que lanzaremos el raycast
    public string libroTag = "Libro"; // El tag del objeto que deseas detectar
    public Image imagenCanvas; // La imagen en el canvas que se activar�
    public string escenaDestino; // El nombre de la escena a la que deseas cambiar
    public float longitudRaycast = 5f; // Longitud del raycast (ajusta seg�n tu escala)

    private bool libroDetectado = false;

    void Update()
    {
        // Lanza un raycast desde el centro de la c�mara con longitud reducida
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        ray.direction *= longitudRaycast; // Multiplica la direcci�n por la longitud deseada
        RaycastHit hit;

        // Dibuja el raycast en el escenario para depuraci�n
        Debug.DrawRay(ray.origin, ray.direction, Color.green);

        // Comprueba si el raycast colisiona con un objeto con el tag "Libro"
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag(libroTag))
        {
            // El raycast toca el objeto con el tag "Libro", por lo que activamos la imagen en el canvas
            imagenCanvas.gameObject.SetActive(true);
            libroDetectado = true;

            // Comprueba si se presiona la tecla "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                CambiarEscena();
            }
        }
        else
        {
            // Si el raycast no toca el objeto con el tag "Libro", desactivamos la imagen en el canvas
            if (libroDetectado)
            {
                imagenCanvas.gameObject.SetActive(false);
                libroDetectado = false;
            }
        }
    }

    void CambiarEscena()
    {
        // Cambia a la escena especificada
        SceneManager.LoadScene(escenaDestino);
    }
}

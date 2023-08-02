using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaDespuesDeTiempo : MonoBehaviour
{
    public float tiempoEspera = 3f;

    private void Start()
    {
        Invoke("CambiarEscena", tiempoEspera);
    }

    private void CambiarEscena()
    {
        SceneManager.LoadScene(0); // Cargar la Scene número 0 después de 3 segundos.
    }
}

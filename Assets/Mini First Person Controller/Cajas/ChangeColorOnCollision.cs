using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    public Color collisionColor = Color.red; // Color a cambiar cuando hay una colisión con un objeto que tenga el tag "cosa"
    private Color originalColor; // Color original del objeto
    private Renderer renderer;
    private Rigidbody rigidbody; // Referencia al Rigidbody del objeto
    private bool isRed = false; // Bandera para rastrear si el objeto es rojo

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color; // Guardamos el color original del objeto al inicio
        rigidbody = GetComponent<Rigidbody>(); // Obtenemos el componente Rigidbody
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si el objeto con el que colisionamos tiene el tag "cosa"
        if (collision.gameObject.CompareTag("Cosa") || collision.gameObject.CompareTag("Pickable"))
        {
            // Cambiamos temporalmente el color del objeto a collisionColor
            renderer.material.color = collisionColor;
            isRed = true;
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            // Cambiamos temporalmente el color del objeto a collisionColor
            isRed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verificamos si dejamos de colisionar con un objeto que tenga el tag "cosa"
        if (collision.gameObject.CompareTag("Cosa") || collision.gameObject.CompareTag("Pickable"))
        {
            // Restauramos el color original del objeto
            renderer.material.color = originalColor;
            isRed = false;

            // Detenemos la velocidad del objeto al cambiar el color de nuevo al original
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector3.zero; // Detenemos la velocidad del objeto
                rigidbody.angularVelocity = Vector3.zero; // Detenemos la velocidad angular del objeto
            }
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isRed = false;
        }
    }

    public bool IsRed()
    {
        return isRed;
    }
}
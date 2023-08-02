using UnityEngine;

public class DisappearOnKeyPress : MonoBehaviour
{
    // La tecla que activará la desaparición del objeto
    public KeyCode keyToDisappear = KeyCode.E;

    // El objeto que queremos hacer desaparecer
    public GameObject objectToDisappear;

    // Variable para guardar el estado de visibilidad del objeto
    private bool isVisible = true;

    void Update()
    {
        // Verificamos si se ha presionado la tecla
        if (Input.GetKeyDown(keyToDisappear))
        {
            // Cambiamos el estado de visibilidad del objeto
            isVisible = !isVisible;
            // Aplicamos el cambio al objeto
            objectToDisappear.SetActive(isVisible);
        }
    }
}

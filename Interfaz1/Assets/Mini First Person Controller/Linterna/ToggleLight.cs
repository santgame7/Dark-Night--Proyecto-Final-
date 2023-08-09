using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light targetLight; // Referencia a la luz que deseas controlar

    private void Update()
    {
        // Verificar si se presionó la tecla F
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Cambiar el estado de la luz entre encendida y apagada
            targetLight.enabled = !targetLight.enabled;
        }
    }
}

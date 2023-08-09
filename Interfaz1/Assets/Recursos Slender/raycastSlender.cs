using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastSlender : MonoBehaviour
{
    // El objeto del jugador
    public GameObject playerObj;

    // Transform de Slender
    public Transform slenderTransform;

    // Booleano que determina si el raycast est� golpeando al jugador o no
    public bool detected;

    // Desplazamiento que ayuda a posicionar el raycast si no est� ubicado correctamente
    public Vector3 offset;

    // El m�todo Update() realiza acciones en cada frame
    void Update()
    {
        Vector3 direction = (playerObj.transform.position - slenderTransform.position).normalized; // La direcci�n del raycast de Slender apuntar� hacia el jugador
        RaycastHit hit; // Variable RaycastHit

        // Si el raycast golpea algo,
        if (Physics.Raycast(slenderTransform.position + offset, direction, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(slenderTransform.position + offset, hit.point, Color.red, Mathf.Infinity); // El raycast se dibuja con fines de visualizaci�n en el Editor de Unity
            if (hit.collider.gameObject == playerObj) // Si el raycast golpea el objeto del jugador,
            {
                detected = true; // detected es verdadero
            }
            else // en caso contrario,
            {
                detected = false; // detected es falso
            }
        }
    }
}

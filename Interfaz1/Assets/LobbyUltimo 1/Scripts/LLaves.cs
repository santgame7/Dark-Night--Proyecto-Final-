using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLaves : Interactuable
{
    [SerializeField] GameObject _llave;
    private void Start()
    {
        _llave = this.gameObject;
    }
    public override void Interact()
    {
        base.Interact();

        // Eliminar el componente LLaves del objeto actual
        Destroy(_llave);
    }
}
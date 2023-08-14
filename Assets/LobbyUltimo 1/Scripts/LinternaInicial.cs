using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternaInicial : Interactuable
{
    [SerializeField] PlayerMovement plyr;

    [SerializeField] GameObject _linternaInicial;
    [SerializeField] GameObject _linternaFinal;

    // Start is called before the first frame update
    void Start()
    {
        _linternaFinal.SetActive(false);
    }

    public override void Interact()
    {
        Destroy(_linternaInicial);
        _linternaFinal.SetActive(true);
        plyr.AgarroLinterna = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
using TMPro;
//

public class LogicaFullScreen : MonoBehaviour
{
    public Toggle toggle;

    //using TMPro abilita la opcion desplegable de la resolucion
    public TMP_Dropdown resolucionesDropDown;
    //con esto se crea resoluciones disponibles en el pc
    Resolution[] resoluciones;
    //

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        //
        RevisarResolucion();
        //
    }


    void Update()
    {

    }

    public void ActiveFULLS(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;

    }

    //
    public void RevisarResolucion()
    {
        // esta fincion borra las resoluciones que trea el modificador de unity y las reemplaza por las que tre el pc dedl usuario, crea una cantidad de resoluciones dependiendo de la cantidad de resoluciones que tenga el pc del usuario es deci  que si ewl computador aguanta solo 10 resoluciones solo se generaran 10 en el desplegable de resoluciones 

        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        //mide la pantalla altura y anchura y se guarda en la lista que se genero anteriormente
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            //esto genera la resolucion del juego en la lista y la guarda
            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }

        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();


        //
        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
        //
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        //
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);
        //


        Resolution resolution = resoluciones[indiceResolucion];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    //
}
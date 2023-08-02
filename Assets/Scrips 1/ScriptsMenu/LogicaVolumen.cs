using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaVolumen : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image imagenMute;

    void Start()
    {
        //hce que el slider inicie en un valor definido a la mitad
        slider.value = PlayerPrefs.GetFloat("volumenRadio", 0.5f);
        //controla el volumen de 1 a 100 %
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();

    }
    public void ChangeSlider(float valor)
    {
        //esta parte tendra el valor de la barra de sonido del juego
        sliderValue = valor;
        //esto controla el valor que queremos que tenga la barra de sonido
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        //y esto sera el valor que tendra al final 
        AudioListener.volume = slider.value;
        //revisa si el audio esta en mute y activa la imagen de mute
        RevisarSiEstoyMute();

    }

    //esta funcion revisa si el volumen esta en 0 y se activa una imagen que indica que esta en mute
    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imagenMute.enabled = true;
        }
        else
        {
            imagenMute.enabled = false;
        }
    }

    
}

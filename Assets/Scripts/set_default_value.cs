using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class set_default_value : MonoBehaviour
{


    public Slider slider;
    public void change_value()
    {
        slider.value =  AudioListener.volume;
    }
    void Start()
    {
    change_value();
    }
}

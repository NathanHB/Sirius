using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handleVolume : MonoBehaviour
{
    
    public Slider Volume;
    void Update()
    {
        AudioListener.volume = Volume.value;
    
    }
}

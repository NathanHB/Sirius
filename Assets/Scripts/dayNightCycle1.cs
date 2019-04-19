using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle1 : MonoBehaviour
{
    public Light sun; 
    public ParticleSystem stars;
    public GameObject starsObj;

       


    // Update is called once per frame
    void Update()
    {
        OrbitSun(); 
    }
    
    void OrbitSun(){
        
        transform.RotateAround(new Vector3(250,250,0), Vector3.forward,  2f*Time.deltaTime);        
    }
}

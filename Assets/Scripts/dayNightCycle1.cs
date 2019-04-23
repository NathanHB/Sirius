using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle1 : MonoBehaviour
{
    public Light sun; 
    public ParticleSystem stars;
    public GameObject starsObj;
    private float timer=0;
       


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        OrbitSun(); 
    }
    
    void OrbitSun(){
        
        transform.RotateAround(new Vector3(250,250,0), Vector3.forward,  Time.deltaTime);        
    }
}

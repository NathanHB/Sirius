using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle1 : MonoBehaviour
{
    public Light sun; 
  
    private float timer=0;
    private bool isNight = false;
       


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        OrbitSun(); 
    }
    
    void OrbitSun()
    {

        if (sun.transform.position.y < 0)
        {
            if (!isNight) isNight = true;
            if (sun.transform.position.y < -50)
                sun.intensity = 0f;
            else
            {
                sun.intensity = 1;
            }
        }
        else
        {
            if (isNight) isNight = false;
        }


        
        //Debug.Log(isNight);
        
        transform.RotateAround(new Vector3(250,250,0), Vector3.forward,  3f*Time.deltaTime);        
    }
}

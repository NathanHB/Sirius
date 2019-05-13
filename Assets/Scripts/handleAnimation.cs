using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class handleAnimation : NetworkBehaviour
{
    [SerializeField] private GameObject villager;
    private bool isWalking;
    private bool isRunning;
    void Start()
    { 
     isWalking = false;
    }
    
    
    
    public void handlingAnimation(float velocity)
    {
        if (!isLocalPlayer)
            return;
        
        if (velocity>0)
        {
            if (velocity<6)
            {
                if (!isWalking){
                    villager.GetComponent<Animator>().SetBool("walking", true);
                    isWalking = true;
                    
                    villager.GetComponent<Animator>().SetBool("running", false);
                    isRunning = false;
                }
            }  
            else if (velocity > 6)
            {
                if (!isRunning)
                {
                    villager.GetComponent<Animator>().SetBool("running", true);
                    isRunning = true;
                    
                    villager.GetComponent<Animator>().SetBool("walking", false);
                    isWalking = false;
                }
            }
        }
        else
        {
            villager.GetComponent<Animator>().SetBool("walking", false);
            isWalking = false;
            villager.GetComponent<Animator>().SetBool("running", false);
            isRunning = false;

        }
    }
}

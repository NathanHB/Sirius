using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleAnimation : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] private GameObject villager;
    [SerializeField] private GameObject player;
    private Vector3 velocity;
    private bool isWalking;
    private bool isRunning;
    void Start()
    { 
     isWalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = player.GetComponent<PlayerMotor>().velocity;

        if (velocity.magnitude>0)
        {
            if (!isWalking)
            {
                villager.GetComponent<Animator>().SetBool("walking", true);
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                villager.GetComponent<Animator>().SetBool("walking", false);
                isWalking = false;
            }
            
        }

        if (velocity.magnitude > 6)
        {
            if (!isRunning)
            {
                villager.GetComponent<Animator>().SetBool("running", true);
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                villager.GetComponent<Animator>().SetBool("running", false);
                isRunning = false;
            }
            
        }
        
        
    }
}

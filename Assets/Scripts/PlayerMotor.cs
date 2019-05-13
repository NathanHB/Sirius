using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerObjectSetup))]

public class PlayerMotor : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    public Vector3 velocity = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 camRot = Vector3.zero;
    [SerializeField] private GameObject villager;
    private handleAnimation animHandler;
    

    
    private Rigidbody rb;

    private bool isWalking;
    private bool isRunning;
    
   


    public AudioSource jumpSound;
    

    void Start()
    {
        isWalking = false;
        isRunning = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;//Prevent the damn rb to spin after colliding a wall
        animHandler = GetComponent<handleAnimation>();
    }

    private void Update()
    {
        //animHandler.handlingAnimation(velocity.magnitude);
        handleAnimation(velocity.magnitude);
    }


    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        if (hasAuthority) rotation = _rotation;
        else rotation = Vector3.zero;
    }

    public void RotateCamera(Vector3 _camRot)
    {
        if ((cam.transform.eulerAngles.x >= 0 && cam.transform.eulerAngles.x <= 60) ||
            (cam.transform.eulerAngles.x >= 320 && cam.transform.eulerAngles.x <= 360))
        {
            camRot = _camRot;
        }
        else
        {
            if (cam.transform.eulerAngles.x > 60 && cam.transform.eulerAngles.x < 180)
                cam.transform.eulerAngles = new Vector3( 59.99f,cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);     
            else if (cam.transform.eulerAngles.x >= 180 && cam.transform.eulerAngles.x < 320)
                cam.transform.eulerAngles = new Vector3(320.01f, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
                
        }

    }

    void FixedUpdate()
    { 
        if (!hasAuthority) return;
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {
            if (rotation != Vector3.zero)
            {   
                //Debug.Log("turning");
                rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
            
            }
            cam.transform.Rotate(-camRot);
        

       // Debug.Log("Camera rotating");
        
    }


    public void jump()
    {
            jumpSound.Play();
            rb.AddForce(Vector3.up*400, ForceMode.Impulse);
            villager.GetComponent<Animator>().SetBool("jumping", true);
    }

    public override void OnStartLocalPlayer()  
    {  
        NetworkAnimator netAnim = villager.GetComponent<NetworkAnimator>();  
      
        netAnim.SetParameterAutoSend(0, true);  
        netAnim.SetParameterAutoSend(1, true);  
        netAnim.SetParameterAutoSend(2, true);  
        netAnim.SetParameterAutoSend(3, true);  

    }  
  
    public override void PreStartClient()  
    {   
        NetworkAnimator netAnim = villager.GetComponent<NetworkAnimator>();  
        netAnim.SetParameterAutoSend(0, true); 
        netAnim.SetParameterAutoSend(1, true);  
        netAnim.SetParameterAutoSend(2, true);  
        netAnim.SetParameterAutoSend(3, true);  

    }  



    
    
    void handleAnimation(float velocity)
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

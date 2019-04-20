using System;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerObjectSetup))]

public class PlayerMotor : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRot = Vector3.zero;

    private Rigidbody rb;
    private float distToGround;

    public GameObject uiHandler;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;//Prevent the damn rb to spin after colliding a wall
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        if (uiHandler.GetComponent<handle_ui_ingame>().gamePaused){//if game is paused
            velocity = Vector3.zero;
            rotation=Vector3.zero;
            camRot=Vector3.zero;     
            return;
        }

        if (hasAuthority) rotation = _rotation;
        else rotation = Vector3.zero;
    }

    public void RotateCamera(Vector3 _camRot)
    {
        if (uiHandler.GetComponent<handle_ui_ingame>().gamePaused){//if game is paused
            velocity = Vector3.zero;
            rotation=Vector3.zero;
            camRot=Vector3.zero;     
            return;
        }

        if ((cam.transform.eulerAngles.x >= 0 && cam.transform.eulerAngles.x <= 60) ||
            (cam.transform.eulerAngles.x >= 320 && cam.transform.eulerAngles.x <= 360))
        {
            camRot = _camRot;
        }
        else
        {
            if (cam.transform.eulerAngles.x>60 && cam.transform.eulerAngles.x<180)
                cam.transform.eulerAngles = new Vector3( 59.99f,cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);     
            else if(cam.transform.eulerAngles.x>=180 && cam.transform.eulerAngles.x<320)
                cam.transform.eulerAngles = new Vector3( 320.01f,cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
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

            rb.AddForce(Vector3.up*400, ForceMode.Impulse);

    }



}

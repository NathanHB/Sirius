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
        if (hasAuthority) rotation = _rotation;
        else rotation = Vector3.zero;
    }

    public void RotateCamera(Vector3 _camRot)
    {
        camRot = _camRot;
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

       // Debug.Log("Camera rotating");
        cam.transform.Rotate(-camRot);
        
    }


    public void jump()
    {

            rb.AddForce(Vector3.up*500, ForceMode.Impulse);

    }



}

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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        PerfomeMovement();
        PerformeRotation();
    }

    void PerfomeMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformeRotation()
    {
        if (rotation != Vector3.zero)
        {
            Debug.Log("turning");
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        Debug.Log("Camera rotating");
        cam.transform.Rotate(-camRot);
        
    }
}

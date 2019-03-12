using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerObjectSetup))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 camRot = Vector3.zero;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponent<Camera>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _camRot)
    {
        camRot = _camRot;
    }

    void FixedUpdate()
    {
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
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        if (cam != null)
        {
            Debug.Log("Camera rotating");
            cam.transform.Rotate(-camRot);
        }
    }
}

using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerControl : NetworkBehaviour

{
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSensitivity = 3;
    [SerializeField] private float cameraSensitivity = 3;

    private PlayerMotor _motor;

    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
    }

    public void Update()
    {
        if (!hasAuthority) return;

        Debug.Log("I have authority");
        // get movement input from the keyboard
        float Xmov = Input.GetAxisRaw("Horizontal");
        float Zmov = Input.GetAxisRaw("Vertical");

        //apply movement input
        Vector3 movHorizontal = transform.right * Xmov;
        Vector3 movVertical = transform.forward * Zmov;
        Vector3 Velocity = (movHorizontal + movVertical).normalized * speed;
        _motor.Move(Velocity);

        // get Yrotation input for turning 
        float xrot = Input.GetAxisRaw("Mouse X");

        // apply Yrotation Inuput
        Vector3 xrotation = new Vector3(0, xrot, 0) * rotationSensitivity;
        _motor.Rotate(xrotation);

        // get and apply Yrotation input
        float yrot = Input.GetAxisRaw("Mouse Y");
        Vector3 yrotation = new Vector3(yrot, 0, 0) * cameraSensitivity;
        _motor.RotateCamera(yrotation);

    }
}

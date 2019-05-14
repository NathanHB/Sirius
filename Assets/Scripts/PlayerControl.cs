using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerControl : NetworkBehaviour

{
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSensitivity = 3;
    [SerializeField] private float cameraSensitivity = 3;
    private float Xmov = 0;
    private float Zmov = 0;
    private Vector3 movHorizontal;
    private Vector3 movVertical;
    private Vector3 Velocity;
    private float xrot;
    private Vector3 xrotation;
    private float yrot;
    private Vector3 yrotation;
    
    private PlayerMotor _motor;


    void Start()
    {     
        _motor = GetComponent<PlayerMotor>();

    }

    public void Update()
    {
        // get movement input from the keyboard

        Xmov = Input.GetAxis("Horizontal");

        Zmov = Input.GetAxis("Vertical");

        //apply movement input
        movHorizontal = transform.right * Xmov;
        movVertical= transform.forward * Zmov;
        Velocity = (movHorizontal + movVertical).normalized * speed;

        // get Yrotation input for turning 
        xrot = Input.GetAxis("Mouse X");

        // apply Yrotation Inuput
        xrotation = new Vector3(0, xrot, 0) * rotationSensitivity;

        // get and apply Yrotation input
        yrot = Input.GetAxis("Mouse Y");
        yrotation = new Vector3(yrot, 0, 0) * cameraSensitivity;

        _motor.Move(Velocity);
        _motor.Rotate(xrotation);
        _motor.RotateCamera(yrotation); 
       
        generalControls();
        
    }


    void generalControls()
    {
        run();
        

        //Debug.DrawRay(transform.position+new Vector3(0,0.1f,0), -Vector3.up, Color.red);
        //Debug.Log(Physics.Raycast(transform.position, Vector3.down+new Vector3(0,2,0), 0.1f, 1<<9));
        
        if (Physics.Raycast(transform.position+new Vector3(0,0.1f,0), -Vector3.up, 0.5f) && Input.GetKeyDown(KeyCode.Space))
        {
          jump();
        }
    }


    private void jump()
    {
           _motor.jump(); 
    }

    void run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed = 12;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 5;
    }

}

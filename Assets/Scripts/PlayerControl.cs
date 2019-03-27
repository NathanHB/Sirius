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

    private PlayerMotor _motor;

    void Start()
    {
        _motor = GetComponent<PlayerMotor>();
    }

    public void Update()
    {
        if (!hasAuthority) return;

        //Debug.Log("I have authority");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

        doorInteract();
    }



    private void doorInteract()
    {
        RaycastHit hit;
        int maxDistance = 5;
        bool isMoving = false;
            
         if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            if (hit.collider.gameObject.CompareTag("door"))
            {
               
                    
                if (Input.GetKey(KeyCode.E) && !isMoving)
                {
                    isMoving = true;
                 StartCoroutine(RotateAround(hit, Vector3.up, 90.0f, 1.0f ));
                 //hit.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
                 hit.transform.tag = "openedDoor";
                 isMoving = false;
                }
            }
            else if (hit.collider.gameObject.CompareTag("openedDoor"))
            {
                if (Input.GetKey(KeyCode.E) && !isMoving)
                {
                    isMoving = true;
                    StartCoroutine(RotateAround(hit, Vector3.up, -90.0f, 1.0f));
                    //hit.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
                    hit.transform.tag = "door";
                    isMoving = false;
                }
            }
            
        }
    }
    
    IEnumerator RotateAround(RaycastHit objectToMove ,Vector3 axis, float angle, float duration )
    {
        
        float elapsed = 0.0f;
        float rotated = 0.0f;
        while( elapsed < duration )
        {
            float step = angle / duration * Time.deltaTime;
            objectToMove.transform.Rotate(Vector3.up, step);
           // objectToMove.transform.RotateAround(transform.position, axis, step );
            elapsed += Time.deltaTime;
            rotated += step;
            yield return null;
        }
        objectToMove.transform.Rotate(Vector3.up, angle-rotated);

        //objectToMove.transform.RotateAround(transform.position, axis, angle - rotated );
    }
}

using UnityEngine;

public class handle_ui_ingame : MonoBehaviour
{
    public bool gamePaused;
 
    public static GameObject PauseMenuUi;
    public static GameObject PauseSettingsUi;

    [SerializeField] private GameObject player;
    //private static Rigidbody rb;
    private static PlayerMotor motor;
    private void Start()
    {
        gamePaused = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        PauseMenuUi = FindObjectsOfType<Canvas>()[1].gameObject;
        PauseSettingsUi = FindObjectsOfType<Canvas>()[2].gameObject;

        PauseMenuUi.SetActive(false);
        PauseSettingsUi.SetActive(false);

        motor = player.GetComponent<PlayerMotor>();
        //rb = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
       // Debug.Log(gamePaused+rb.GetInstanceID().ToString());
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        if (gamePaused)
        {
            motor.rotation=Vector3.zero;
            motor.velocity=Vector3.zero;
            motor.camRot=Vector3.zero;            
        }

    }


    public void ResumeGame()
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        PauseMenuUi.SetActive(false); 
        PauseSettingsUi.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
    }

    void PauseGame()
    {
        
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenuUi.SetActive(true);
        gamePaused = true;
    }

    public void switchToSettings()
    {
        PauseMenuUi.SetActive(false);
        PauseSettingsUi.SetActive(true);
    }

    public void switchToPauseMenu()
    {
        PauseSettingsUi.SetActive(false);
        PauseMenuUi.SetActive(true);   
    }
    
    public void change_main_volume(float value)
    {
        AudioListener.volume = value;
    
    }
   
}

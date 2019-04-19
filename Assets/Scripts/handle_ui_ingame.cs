using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class handle_ui_ingame : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject PauseMenuUi;
    public GameObject PauseSettingsUi;
    //public FirstPersonController character;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (gamePaused)
                ResumeGame();
            else
                PauseGame();
        }
    }


    public void ResumeGame()
    {
        //character.ChangeMouseSensitivity(2,2);
        PauseMenuUi.SetActive(false); 
        PauseSettingsUi.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
    }

    void PauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenuUi.SetActive(true);
        //character.ChangeMouseSensitivity(0,0);
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

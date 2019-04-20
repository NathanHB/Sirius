using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class handle_ui_ingame : MonoBehaviour
{
    public bool gamePaused = false;
    
    public static GameObject PauseMenuUi;
    public static GameObject PauseSettingsUi;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        PauseMenuUi = FindObjectsOfType<Canvas>()[1].gameObject;
        PauseSettingsUi = FindObjectsOfType<Canvas>()[2].gameObject;

        PauseMenuUi.SetActive(false);
        PauseSettingsUi.SetActive(false);
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


    void ResumeGame()
    {
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

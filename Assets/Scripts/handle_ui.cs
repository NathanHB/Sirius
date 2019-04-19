using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handle_ui : MonoBehaviour
{

    
    public void start_online_game()
    {
        Debug.Log("Starting Online Game");
    }

    public void start_offline_game()
    {
        Debug.Log("Starting Offline Game");
    }

    public void exit_game()
    {
        Application.Quit();
    }


    
   
}

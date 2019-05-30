using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class handleTimerDisplay : NetworkBehaviour
{
    private int mainTimer = 0;
    private string state;

    public Text textTDP;
    public Canvas gameOverCanvas;
    public Text gameOverText;
    

    // Update is called once per frame
    void Update()
    {
        (state, mainTimer) = timer.getStateAndTimeLeft();
        
        if (state == "dayVoting")
                textTDP.text = mainTimer + "s before end of vote";
        else if (state=="dayNotVoting")
            textTDP.text = mainTimer+ "s before night";
        else if (state == "night")
            textTDP.text = mainTimer+ "s before end of night";
        else if(state=="preState")
            textTDP.text = "Waiting for all players to connect";
        else
        {
            textTDP.text = "";
            gameOverCanvas.gameObject.SetActive(true);

            if (timer.getWinner()=="Werewolf")
                gameOverText.text ="Werewolves have won !";
            else 
                gameOverText.text = "Villagers have won !";
        }
    }
}

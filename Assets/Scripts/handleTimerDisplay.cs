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
    

    // Update is called once per frame
    void Update()
    {
        (state, mainTimer) = timer.getStateAndTimeLeft();
        if (state == "dayVoting")
                textTDP.text = mainTimer + "s before end of vote";
        else if (state=="dayNotVoting")
            textTDP.text = mainTimer+ "s before night";
        else
            textTDP.text = mainTimer+ "s before end of night";   
    }
}

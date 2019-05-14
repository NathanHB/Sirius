using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Networking;

public class timer : MonoBehaviour
{
    // Start is called before the first frame update

    private static float mTimer;
    private bool start;
    
    public Material skyboxNight;
    public Material skyboxDay;
    
    public GameObject sun;

    private static int dayDuration = 60;
    private static int nightDuration = 60;
    private static int votingProcessDuration = 30;

    public static bool isDay = true;
    private static bool isVoting = false;
    void Start()
    {
        mTimer = 0;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            mTimer += Time.deltaTime;
        else
        {
            if (gameMaster.allPlayersConnected())
                start = true;
        }

        if (isDay && mTimer>dayDuration)
        {
            disableSun();
            isDay = false;
            mTimer = 0;
        }
        else if (!isDay && mTimer > nightDuration)
        {
            enableSun();
            isDay = true;
            mTimer = 0;
        }
        else if (isDay && mTimer < votingProcessDuration)
        {
            if (!isVoting)
                isVoting = true;
        }
        else if(isDay && mTimer > votingProcessDuration)
        {
            if (isVoting)
                isVoting = false;
        }
    }

    public static (string, int) getStateAndTimeLeft()
    {
        if (isDay)
        {
            if (isVoting)
                return ("dayVoting", votingProcessDuration-(int)mTimer);
            return ("dayNotVoting", dayDuration-(int)mTimer);
        }
        return ("night", nightDuration-(int)mTimer);
    
    }


    public void disableSun()
    {
        sun.GetComponent<Light>().intensity = 0;
        RenderSettings.skybox = skyboxNight;
        RenderSettings.fogDensity = 0.04f;
    }
    
    public void enableSun()
    {
        sun.GetComponent<Light>().intensity = 1;
        RenderSettings.skybox = skyboxDay;
        RenderSettings.fogDensity = 0.02f;
    }
}

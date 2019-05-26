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
    public GameObject[] items;

    private static int dayDuration = 10;
    private static int nightDuration = 30;
    private static int votingProcessDuration = 5;
    // IMPORTANT NOTE : dayDuration includes votingProcessDuration
    

    public static bool isDay = true;
    private static bool isVoting = false;
    private bool firstDay;
    private string state;
    void Start()
    {
        mTimer = -1;
        start = false;
        items = GameObject.FindGameObjectsWithTag("Item");
        firstDay = true;
        state = "preState";
        
        Debug.Log("items : "+items.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            mTimer += Time.deltaTime;
        else
        {
            if (gameMaster.allPlayersConnected())
            {
                mTimer = 0;
                start = true;
                state = "dayNotVoting";
            }
            else
                return;
        }
        

        if (firstDay)
        {
            mTimer += votingProcessDuration;
            firstDay = false;
            return;
        }

        if (state=="dayNotVoting" && mTimer>dayDuration)
        {
            Debug.Log("NONOO");
            disableSun();
            disableItems();
            isDay = false;
            state = "night";
            mTimer = 0;
        } 
        else if (state=="night" && mTimer > nightDuration)
        {
            items = GameObject.FindGameObjectsWithTag("Item");
            enableSun();
            enableItems();
            isDay = true;
            state = "dayVoting";
            mTimer = 0;
        }
        else if (state == "dayVoting" && mTimer < votingProcessDuration)
        {
            if (!isVoting)
                isVoting = true;
        }
        else if(state == "dayVoting" && mTimer > votingProcessDuration)
        {
            state = "dayNotVoting";
            if (isVoting)
                isVoting = false;
        }
    }

    public static (string, int) getStateAndTimeLeft()
    {
        if (mTimer==-1)
            return ("preState", 0);
        
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

    public void disableItems()
    {
        foreach (var item in items)
            item.SetActive(false);
    }

    public void enableItems()
    {
        foreach (var item in items)
            item.SetActive(true);  
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMesh idDisplay;
    public GameObject player;
    private bool isIdDisplayEnabled = true;
    private string currentState;

    void Start()
    {
        string toDp = player.name;
        toDp = 'P' + toDp.Substring(1);
        idDisplay.text = toDp;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = timer.getStateAndTimeLeft().Item1;

        if (currentState=="night" && isIdDisplayEnabled)
        {
            idDisplay.gameObject.SetActive(false);
            isIdDisplayEnabled = false;
        }
        else if (currentState != "night" && !isIdDisplayEnabled)
        {
            idDisplay.gameObject.SetActive(true);
            isIdDisplayEnabled = true;
        }

    }
}

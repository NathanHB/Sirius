﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    [SerializeField] private uint roomSize = 6;

    private string roomName = "default";
    private NetworkManager _networkManager;
    private string password = "";

    public void SetRoomName(string roomName)
    {
        this.roomName = roomName;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }
    
    void Start()
    {
        _networkManager = NetworkManager.singleton;
        
        if(_networkManager.matchMaker == null) _networkManager.StartMatchMaker();
        
    }

    public void CreateRoom()
    {
        if(_networkManager.matchMaker == null) _networkManager.StartMatchMaker();
        
        if (!String.IsNullOrEmpty(roomName))
        {
            Debug.Log("Creating room " + roomName);

            _networkManager.matchMaker.CreateMatch(roomName, roomSize, true, password, "", "", 0, 0,
                _networkManager.OnMatchCreate);
        }
    }
}

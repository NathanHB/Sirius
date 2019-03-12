using System;
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

    public void SetRoomName(string _roomName)
    {
        roomName = _roomName;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }
    // Start is called before the first frame update
    void Start()
    {
        _networkManager = NetworkManager.singleton;
        
        if(_networkManager.matchMaker == null) _networkManager.StartMatchMaker();
        
    }

    // Update is called once per frame
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

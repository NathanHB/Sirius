using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
    private List<GameObject> roomList = new List<GameObject>();
    private NetworkManager _networkManager;
    [SerializeField] private Text status;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform scrollView; 
    
    // Start is called before the first frame update
    void Start()
    {
        _networkManager = NetworkManager.singleton;
        if(_networkManager.matchMaker == null) _networkManager.StartMatchMaker();

        Refresh();
    }

    public void Refresh()
    {
        _networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        status.text = "";

        if (matches == null)
        {
            status.text = "Error while loading rooms";
            return;
        }

        ClearRooms();

        foreach (var match in matches)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(scrollView);

            RoomListItem _roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if (_roomListItem != null) _roomListItem.Setup(match, JoinRoom);
            
            roomList.Add(roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms found...";
        }
    }

    void ClearRooms()
    {
        foreach (var room in roomList)
        {
            Destroy(room);
        } 
        
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot match)
    {
        _networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, _networkManager.OnMatchJoined);
        ClearRooms();
        status.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

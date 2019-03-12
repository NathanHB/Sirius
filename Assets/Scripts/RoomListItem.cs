using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    private MatchInfoSnapshot match;

    [SerializeField] private Text roomListText;

    public delegate void JoinRoomDelegate(MatchInfoSnapshot match);

    private JoinRoomDelegate _JoinRoomDelegate;
    
    
    // Start is called before the first frame update
    public void Setup(MatchInfoSnapshot match, JoinRoomDelegate _joinRoom)
    {
        this.match = match;

        _JoinRoomDelegate = _joinRoom;

        roomListText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinGame()
    {
        _JoinRoomDelegate.Invoke(match);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

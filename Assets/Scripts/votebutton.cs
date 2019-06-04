using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class votebutton : MonoBehaviour
{
    private int id;
    [SerializeField] private Text buttonText;

    public delegate void VoteDelagate(int id);

    private VoteDelagate voteDelagate;

    public void Setup(uint id, VoteDelagate vote)
    {
        voteDelagate = vote;
        this.id = (int) id;
        Debug.Log("setting up the button to player: " + (this.id - 6));
        if(buttonText != null) buttonText.text = "Player: " + (id - 6);
    }

    public void Vote()
    {
        Debug.Log("voting");
        voteDelagate.Invoke(id);
    }
}

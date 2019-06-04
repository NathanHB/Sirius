using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class votebutton : MonoBehaviour
{
    private uint id;
    [SerializeField] private Text buttonText;

    public delegate void VoteDelagate(int id);

    private VoteDelagate voteDelagate;

    public void Setup(uint id, VoteDelagate vote)
    {
        voteDelagate = vote;
        this.id = id;
        buttonText.text = "Player: " + (id - 6);
    }

    public void Vote()
    {
        voteDelagate.Invoke((int)id);   
    }
}

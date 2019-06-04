using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class voting : NetworkBehaviour
{
    [SerializeField] private GameObject votingButtonPrefab;

    [SerializeField] private Transform scrollView;
    private bool voted = false;

    // Update is called once per frame
    void Update()
    {
        (string, int) stateTime = timer.getStateAndTimeLeft();
        
        if (stateTime.Item1 == "dayVoting" && !voted)
        {
            Debug.Log("you can vote");
            setupCanvas();
            voted = true;
        }
        else if (stateTime.Item1 != "dayVoting")
        {
            Debug.Log("You can't vote");
            voted = false;
            //clearButtons();
        }
    }

    private void setupCanvas()
    {
        for (int i = 0; i < gameMaster.getPlayersNumber(); i++)
        {
            //if (i + 7 != netId.Value)
            {
                Debug.Log("setup canvas for player: " + i);
                GameObject votingButtonGO = Instantiate(votingButtonPrefab);
                votingButtonGO.transform.SetParent(scrollView);

                votebutton votebutton = votingButtonGO.GetComponent<votebutton>();
                if (votebutton != null) votebutton.Setup((uint)i, gameMaster.cmdVote);
            }
        }
    }

    private void clearButtons()
    {
        int childCount = scrollView.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}

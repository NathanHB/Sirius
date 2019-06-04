using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class voting : NetworkBehaviour
{
    [SerializeField] private GameObject votingButtonPrefab;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private Transform scrollView;
    [SerializeField] private Behaviour controls;
    private bool voted = false;

    void Start()
    {
        setupCanvas();
        clearButtons();
    }

    // Update is called once per frame
    void Update()
    {
        (string, int) stateTime = timer.getStateAndTimeLeft();
        
        if (stateTime.Item1 == "dayVoting" && !voted)
        {
            Debug.Log("you can vote");
            setupCanvas();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            controls.enabled = false;
            voted = true;
        }
        else if (stateTime.Item1 != "dayVoting")
        {
            Debug.Log("You can't vote");
            voted = false;
            clearButtons();
            controls.enabled = true;
        }
    }

    private void setupCanvas()
    {
        for (int i = 0; i < gameMaster.getPlayersNumber(); i++)
        {
            if (i + 7 != GetComponent<NetworkIdentity>().netId.Value)
            {
                Debug.Log("setup canvas for player: " + i);
                GameObject votingButtonGO = Instantiate(votingButtonPrefab);
                votingButtonGO.transform.SetParent(scrollView);

                votebutton votebutton = buttonPrefab.GetComponent<votebutton>();
                if (votebutton != null) votebutton.Setup((uint)i, gameMaster.cmdVote);
            }
        }
    }

    private void clearButtons()
    {
        int childCount = scrollView.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            Destroy(scrollView.transform.GetChild(i).gameObject);
        }
    }
}

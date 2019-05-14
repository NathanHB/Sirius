using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class handleGeneralDisplay : MonoBehaviour
{
    private string role;
    private bool instructionsDisplayed = false;

    public Text textContainer;
    // Start is called before the first frame update
    void Start()
    {
        role = PlayerSetup.getRole();

        textContainer.text = "You are "+role+"!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer.isDay && !instructionsDisplayed)
        {
            textContainer.text = "Press F to turn into a  werewolf";
            instructionsDisplayed = true;
        }
        else if (timer.isDay && instructionsDisplayed)
        {
            textContainer.text = "You are " + role + "!";
            instructionsDisplayed = false;
        }

    }
}

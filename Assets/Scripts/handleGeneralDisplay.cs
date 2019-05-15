using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

public class handleGeneralDisplay : MonoBehaviour
{
    private string role;
    private bool instructionsDisplayed = false;
    private bool isTransformed = false;
    private bool isVisionDark = false;

    public Canvas darkVision;

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
        if (role=="Werewolf")
        {
            if (werewolfActions.getTransformedState())
            {
                textContainer.text = "Press F to return to villager";
                isTransformed = true;
                return;
            }
            else if (!werewolfActions.getTransformedState() && isTransformed)
            {
                textContainer.text = "Press F to turn into a  werewolf";
                isTransformed = false;
                return;
            }


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
        else
        {
            if (!timer.isDay && !instructionsDisplayed)
            {
                textContainer.text = "RUN !";
                instructionsDisplayed = true;
            }
            else if (timer.isDay && instructionsDisplayed)
            {
                textContainer.text = "You are " + role + "!";
                instructionsDisplayed = false;
            }

            if (!timer.isDay && !isVisionDark)
            {
                darkVision.gameObject.SetActive(true);
                isVisionDark = true;
            }
            else if(timer.isDay && isVisionDark)
            {
                darkVision.gameObject.SetActive(false);
                isVisionDark = false;
            }
                
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class handleGeneralDisplay : MonoBehaviour
{
    private string role;
    private bool instructionsDisplayed = false;
    private bool isTransformed = false;
    private bool isVisionDark = false;
    private bool isIdDisplayEnabled = true;

    public GameObject player;

    public Canvas darkVision;
    private bool hasSubClass = false;
    private string subClass = "";
    private string currentState = "";

    public TextMesh idDisplay;
    public Text textContainer;
    // Start is called before the first frame update
    void Start()
    {
        role = player.tag;
        string toDp = player.name;
        toDp = 'P' + toDp.Substring(1);
        idDisplay.text = toDp;
        textContainer.text = "You are "+role+"!";
    }

    // Update is called once per frame
    void Update()
    {

        currentState = timer.getStateAndTimeLeft().Item1;
                
        if (!hasSubClass)
        {
            string sClass = PlayerSetup.getSubClass();
            if (sClass.Length > 0)
            {
                hasSubClass = true;
                subClass = sClass;
            }      
        }

        if (currentState=="preState")
        {
            textContainer.text = "You are "+PlayerSetup.getRole()+" !";
            instructionsDisplayed = true;
            return;             
        }
        
        if (currentState == "night" && isIdDisplayEnabled)
        {
            idDisplay.gameObject.SetActive(false);
            isIdDisplayEnabled = false;
        }

        if (currentState == "dayVoting")
        {
           textContainer.text = "Please vote for a suspect player to kill";
           instructionsDisplayed = true;
           if (!isIdDisplayEnabled)
           {
               idDisplay.gameObject.SetActive(true);
               isIdDisplayEnabled = true;
           }
           
           if( isVisionDark)
           {
               darkVision.gameObject.SetActive(false);
               isVisionDark = false;
           }
           return;
        }
        

        // Debug.Log(role);
        
        
        if (role=="Werewolf")
        {
            if (werewolfActions.getTransformedState())
            {
                textContainer.text = "Press F to return to villager";
                isTransformed = true;
                return;
            }
            if (!werewolfActions.getTransformedState() && isTransformed)
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
                textContainer.text = "You are Werewolf!";
                instructionsDisplayed = false;
            }
        }
        else
        {
            if (!hasSubClass)
            {
                 if (currentState=="night" && !instructionsDisplayed)
                 {
                  textContainer.text = "RUN !";
                  instructionsDisplayed = true;
                 }
                 else if (currentState=="dayNotVoting" && instructionsDisplayed)
                 {
                  textContainer.text = "You are villager!";
                  instructionsDisplayed = false;
                 }
            }
            else
            {
                if (currentState=="night" && !instructionsDisplayed)
                {
                    if (subClass == "hunter")
                        textContainer.text = "You are a hunter. You can kill a werewolf by left-clicking";
                    else if (subClass == "wizard")
                        textContainer.text = "You are a wizard. You will be able to save or kill players";
                    else
                        textContainer.text = "You are a little girl. You can run faster and see better at night";  
                    
                    instructionsDisplayed = true;
                }
                else if (currentState=="dayNotVoting")
                {
                    textContainer.text = "You are a "+subClass+"!";
                    instructionsDisplayed = false;
                }   
            }
                

            if (currentState=="night" && !isVisionDark)
            {
                darkVision.gameObject.SetActive(true);
                isVisionDark = true;
            }

                
        }


    }
}

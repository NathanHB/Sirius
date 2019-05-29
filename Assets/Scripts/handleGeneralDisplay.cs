using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.UI;

public class handleGeneralDisplay : MonoBehaviour
{
    private string role;
    private bool isTransformed = false;
    private bool isVisionDark = false;

    [SerializeField] GameObject player;
    [SerializeField] GameObject villagerSkin;

    public Canvas darkVision;
    public Canvas voteCanvas;

    private bool hasSubClass = false;
    private string subClass = "";
    private string currentState = "";

  
    [SerializeField] Text textContainer;

    void Start()
    {
        role = gameMaster.getRole("player "+ player.GetComponent<NetworkIdentity>().netId.ToString());

        voteCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        textContainer.text = player.tag;
        /*string toDisp = "";
        toDisp += "n : ";
        string[] playersIds = gameMaster.getPlayersId();
        toDisp += playersIds.Length + "==";

        for (int i = 0; i < playersIds.Length; i++)
        {
            toDisp += playersIds[i];
        }

        textContainer.text = toDisp;*/

        //textContainer.text = gameMaster.getPlayersNumber()+"";
        //gameMaster.getRole("player " + player.GetComponent<NetworkIdentity>().netId.ToString()); 
        /*
        currentState = timer.getStateAndTimeLeft().Item1;
        
                
        if (!hasSubClass && villagerSkin.tag!="Untagged")
        {
            hasSubClass = true;
            subClass = villagerSkin.tag;
        }
       
        
        if (currentState=="preState")
        {
            textContainer.text = "You are "+role+" !";
        }
        else if (currentState == "night")
        {

            if (role=="Werewolf")
            {
                if (werewolfActions.getTransformedState())//if transformed
                {
                    textContainer.text = "Press F to return to villager";
                    isTransformed = true;
                }
                else
                {
                    textContainer.text = "Press F to turn into a  werewolf";
                    if (isTransformed)
                        isTransformed = false;
                }
            }
            else
            {
                    if (!hasSubClass)
                    {
                       textContainer.text = "RUN !";
                    }
                    else
                    {
                        if (subClass == "hunter")
                            textContainer.text = "You are a hunter. You can kill a werewolf by left-clicking";
                        else if (subClass == "wizard")
                            textContainer.text = "You are a wizard. You will be able to save or kill players";
                        else
                            textContainer.text = "You are a little girl. You can run faster and see better at night";           
                    }
 
                if (!isVisionDark)
                {
                    darkVision.gameObject.SetActive(true);
                    isVisionDark = true;
                }
            }
            
            
        }
        else if (currentState == "dayVoting")
        {
           
           textContainer.text = "Please vote for a suspect player to kill";

           
           if( isVisionDark)
           {
               darkVision.gameObject.SetActive(false);
               isVisionDark = false;
           }

           if (!voteCanvas.gameObject.activeSelf)
               voteCanvas.gameObject.SetActive(true);   
        }
        else if (currentState == "dayNotVoting")
        {
            if (voteCanvas.gameObject.activeSelf)
                voteCanvas.gameObject.SetActive(false);

            if (!hasSubClass)
               textContainer.text = "You are " + role + " !";
            else 
               textContainer.text = "You are " + subClass + " !";
        }
        */

    }
}

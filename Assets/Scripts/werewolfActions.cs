using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class werewolfActions : MonoBehaviour
{
    private bool isWerewolf = PlayerSetup.getRole() == "Werewolf";
    private bool isTransformed = false;

    public GameObject werewolfSkin;
    public GameObject villagerSkin;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isWerewolf)
        {
            Debug.Log("Yeah");
            if (!isTransformed)
                transformToWerewolf();

            if (isTransformed)
                TransformToVillager();    
        }
        
    }


    void transformToWerewolf()
    {
        werewolfSkin.SetActive(true);
        villagerSkin.SetActive(false);
    }

    void TransformToVillager()
    {
        villagerSkin.SetActive(true);
        werewolfSkin.SetActive(false);
    }
}

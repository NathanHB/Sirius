using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class werewolfActions : NetworkBehaviour
{
    private bool isWerewolf;
    private static bool isTransformed;

    public GameObject werewolfSkin;
    public GameObject villagerSkin;
    
    public Avatar villagerAvatar;
    public Avatar werewolfAvatar;
    
    public RuntimeAnimatorController werewolfAnimator;
    public RuntimeAnimatorController villagerAnimator;

    public Camera playerCamera;
    
    private Animator anim;
    public GameObject player;

    private float scdTimer;
    void Start()
    {
        isWerewolf = player.tag == "Werewolf";
        scdTimer = 0;
        anim = GetComponent<Animator>();
        isTransformed = false;
    }
    void Update()
    {
        
        if (isLocalPlayer)
        {
            if (timer.isDay && isTransformed)
            {
                TransformToVillager();
                return;
            }

            if (Input.GetKeyDown(KeyCode.F) && isWerewolf && scdTimer > 1)
            {
                if (!isTransformed)
                    transformToWerewolf();

                else if (isTransformed)
                    TransformToVillager();

                scdTimer = 0;
            }

            scdTimer += Time.deltaTime;


        }
    }


    public static bool getTransformedState()
    {
        return isTransformed;
    }

    void transformToWerewolf()
    {
        Debug.Log("Transforming to werewolf");
        werewolfSkin.SetActive(true);
        villagerSkin.SetActive(false);
        isTransformed = true;

        anim.avatar = werewolfAvatar;
        anim.runtimeAnimatorController = werewolfAnimator as RuntimeAnimatorController;
        
        CmdSetActiveToWerewolf();

        playerCamera.transform.localPosition = new Vector3(0,1.9f, 1);

    }

    void TransformToVillager()
    {
        Debug.Log("Transforming to villager");

        villagerSkin.SetActive(true);
        werewolfSkin.SetActive(false);
        isTransformed = false;

        anim.avatar = villagerAvatar;
        anim.runtimeAnimatorController = villagerAnimator as RuntimeAnimatorController;
        
        CmdSetActiveToVillager();
        
        playerCamera.transform.localPosition = new Vector3(0,1.7f, 0.15f);

    }

    [Command]
    void CmdSetActiveToWerewolf()
    {
        RpcSetActiveToWerewolf();
    }
 
    [ClientRpc]
    void RpcSetActiveToWerewolf()
    {
        werewolfSkin.SetActive(true);
        villagerSkin.SetActive(false);
        anim.avatar = werewolfAvatar;
        anim.runtimeAnimatorController = werewolfAnimator as RuntimeAnimatorController;
    }
    
    [Command]
    void CmdSetActiveToVillager()
    {
        RpcSetActiveVillager();
    }
 
    [ClientRpc]
    void RpcSetActiveVillager()
    {
        villagerSkin.SetActive(true);
        werewolfSkin.SetActive(false);
        anim.avatar = villagerAvatar;
        anim.runtimeAnimatorController = villagerAnimator as RuntimeAnimatorController;
    }



}

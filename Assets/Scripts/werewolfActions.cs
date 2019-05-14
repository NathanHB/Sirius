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




    private Animator anim;

    private float scdTimer;
    void Start()
    {
        isWerewolf = PlayerSetup.getRole() == "Werewolf";
        scdTimer = 0;
        anim = GetComponent<Animator>();
        isTransformed = false;
    }
    void Update()
    {
        if (isLocalPlayer)
        {
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
        werewolfSkin.SetActive(true);
        villagerSkin.SetActive(false);
        isTransformed = true;

        anim.avatar = werewolfAvatar;
        anim.runtimeAnimatorController = werewolfAnimator as RuntimeAnimatorController;
        
        CmdSetActiveToWerewolf();
    }

    void TransformToVillager()
    {
        villagerSkin.SetActive(true);
        werewolfSkin.SetActive(false);
        isTransformed = false;

        anim.avatar = villagerAvatar;
        anim.runtimeAnimatorController = villagerAnimator as RuntimeAnimatorController;
        
        CmdSetActiveToVillager();
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

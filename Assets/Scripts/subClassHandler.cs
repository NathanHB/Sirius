using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class subClassHandler : NetworkBehaviour
{


    [SerializeField] private Camera cam;

    [SerializeField] private int hunterRange = 10;

    [SerializeField] private LayerMask mask;
    public GameObject player;
    public GameObject villagerSkin;

    private bool isHunter;

    private void Start()
    {
        isHunter = villagerSkin.tag == "hunter";
    }

    void Update()
    {
         if (Input.GetButtonDown("Fire1") && villagerSkin.tag == "hunter" && !timer.isDay)
             attack();
   
    }

    void attack()
    {
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, hunterRange, mask))
        {
            if (_hit.collider.tag == "Werewolf")
                CmdPlayerShot(_hit.collider.name);
        }
    }

    [Command]
    void CmdPlayerShot(string playerId)
    {
        PlayerManager player = gameMaster.GetPlayer(playerId);
        player.RpcTakeDmg(50);
        Debug.Log(playerId + " has been shot.");
    }
    
}

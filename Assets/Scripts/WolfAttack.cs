using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class WolfAttack : NetworkBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private PlayerManager player;

    [SerializeField] private int wolfRange = 10;

    [SerializeField] private LayerMask mask;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    [Client]
    void Shoot()
    {
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, wolfRange, mask))
        {
            //if (_hit.collider.tag == "Werewolf") ;
            
                CmdPlayerShot(_hit.collider.name);
        }
    }

    [Command]
    void CmdPlayerShot(string playerId)
    {
        PlayerManager player = gameMaster.GetPlayer(playerId);
        player.RpcTakeDmg(10);
        Debug.Log(playerId + " has been shot.");
    }
}

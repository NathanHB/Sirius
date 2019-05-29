using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class WolfAttack : NetworkBehaviour
{
    [SerializeField] private Camera cam;

    [SerializeField] private int wolfRange = 10;

    [SerializeField] private LayerMask mask;
    public GameObject player;

    private bool isWerewolf;
    private bool isTransformed = false;

    private void Start()
    {
        isWerewolf = player.tag == "Werewolf";
    }

    void Update()
    {
        if (werewolfActions.getTransformedState())
        {
            if (Input.GetButtonDown("Fire1") && isWerewolf)
                attack();
        }
        
       
    }

    void attack()
    {
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, wolfRange, mask))
        {
            if (_hit.collider.tag == "Villager")
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

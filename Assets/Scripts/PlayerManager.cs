using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
    private int hp = 25;
    [SerializeField] private GameObject[] DisableOnDeath;
    [SerializeField] private Collider _col;
    private Camera sceneCam;
        
    private bool isStuned = false;
    
    [SyncVar]
    private bool isDead = false;
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }


    void Update()
    {
        if (!isLocalPlayer) return;
        
        if (Input.GetKeyDown(KeyCode.K)) RpcTakeDmg(99);
        
    }

    [ClientRpc]
    public void RpcTakeDmg(int amount)
    {
        if (isDead) return;
        
        hp -= amount;

        Debug.Log(transform.name + " now have " + hp + " hp.");
        if (hp <= 0) Die();
    }

    private void Die()
    {
        sceneCam = Camera.main;
        isDead = true;
        for (int i = 0; i < DisableOnDeath.Length; i++)
        {
            DisableOnDeath[i].SetActive(false);
        }
        // disable some components to make the player dead but still here
        if (_col != null) _col.enabled = false;
        
        gameMaster.CmdUnregisterPlayer(transform.name);
        
        if (hasAuthority && sceneCam != null)
        {
            Debug.Log("dying with authority");
            sceneCam.gameObject.SetActive(true);
        }
        Debug.Log(transform.name + " is dead.");
    }
}

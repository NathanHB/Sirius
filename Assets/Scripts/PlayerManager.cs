using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField]private int hp;
    [SerializeField] private GameObject[] DisableOnDeath;
    [SerializeField] private Collider _col;
        
    private bool isStuned = false;
    
    [SyncVar]
    private bool isDead = false;
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    [ClientRpc]
    public void RpcTakeDmg(int amount)
    {
        if (isDead) return;
        hp -= amount;

        if (hp <= 0) Die();

        Debug.Log("u dead");
    }

    private void Die()
    {
        isDead = true;

        for (int i = 0; i < DisableOnDeath.Length; i++)
        {
            DisableOnDeath[i].SetActive(false);
        }
        
        // disable some components to make the player dead but still here
        if (_col != null) _col.enabled = false;
        
        Debug.Log(transform.name + " is dead.");
    }
}

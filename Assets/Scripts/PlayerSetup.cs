using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerManager))]

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;
    [SerializeField] private GameObject graphics;
    [SerializeField] private Behaviour[] compsToDisable;
    public GameObject Player;
    private GameObject[] allPlayers;
    
    private string subClass = "";
    
    void Start()
    {
        sceneCamera = Camera.main;

        if (sceneCamera != null)
        {
            Debug.Log("shutting down main cam.");
            sceneCamera.gameObject.SetActive(false);
        }
        
        if (!isLocalPlayer)
        {
            DisableComponent();
            setRemoteLayer();
        }
        
        allPlayers = FindObjectsOfType<GameObject>();

        if (!isServer)
        {
            CmdRequestRole(GetComponent<NetworkIdentity>().netId.ToString(), Player);
            
            for (int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i].name.Contains("player "))
                    CmdRequestRole(allPlayers[i].name.Remove(0,7), allPlayers[i].gameObject);
            }
        }



    }

    private void Update()
    {
        if (!isServer && GetComponent<NetworkIdentity>().netId==netId)
            CmdRequestRole(GetComponent<NetworkIdentity>().netId.ToString(), Player);
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        
        Debug.Log("number : "+gameMaster.getPlayersNumber());

        string netID = GetComponent<NetworkIdentity>().netId.ToString();

        PlayerManager player = GetComponent<PlayerManager>();

        gameMaster.CmdRegisterPlayer(netID, player);
                
    }

   [Command]
   void CmdRequestRole(string netID, GameObject Player)
   {
       string role = gameMaster.getRole("player " + netID);
       
       RpcSetRole(Player, role);
   }

   [ClientRpc]
   void RpcSetRole(GameObject Player, string role)
   {
       Player.tag = role;
   }

    // Called when the object is destroyed
    void OnDisable()
    {
        if(sceneCamera != null) sceneCamera.gameObject.SetActive(true);
        Debug.Log("u dead");
        gameMaster.CmdUnregisterPlayer(transform.name);
    }

    void DisableComponent()
    {
        foreach (var comp in compsToDisable)
            comp.enabled = false;
    }

    void setRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }




    public string getSubClass()
    {
        return subClass;
    }
}

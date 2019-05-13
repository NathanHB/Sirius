using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerManager))]

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;
    [SerializeField] private GameObject graphics;
    [SerializeField] private Behaviour[] compsToDisable;

    private string role;
    
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
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();

        PlayerManager player = GetComponent<PlayerManager>();

        role = gameMaster.CmdRegisterPlayer(netID, player);
        transform.gameObject.tag = role;
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
        {
            comp.enabled = false;
        }
    }

    void setRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }
}

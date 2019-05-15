using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerManager))]

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;
    [SerializeField] private GameObject graphics;
    [SerializeField] private Behaviour[] compsToDisable;
    

    private static string role;
    private static string subClass = "";
    
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

    public static string getRole()
    {
        return role;
    }

    public static void addSubClass(string sClass)
    {
        switch (sClass)
        {
            case "rifleContent":
                subClass = "hunter";
                break;
            case "potion":
                subClass = "wizard";
                break;
            case "teddyBear":
                subClass = "littleGirl";
                break;
            default:
                subClass = "";
                break;
        }  
    }

    public static string getSubClass()
    {
        return subClass;
    }
}

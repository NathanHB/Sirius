using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerManager))]

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;
    [SerializeField] private GameObject graphics;

    private string role;
    
    void Start()
    {
        sceneCamera = Camera.main;

        if (sceneCamera != null) sceneCamera.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(!hasAuthority)
        {
            if (playerCam != null) playerCam.gameObject.SetActive(false);
        }
        else
        {
            if (playerCam != null) playerCam.gameObject.SetActive(true);
            
        }

        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                GetComponent<PlayerManager>().RpcTakeDmg(10);
            }
        }

        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();

        PlayerManager player = GetComponent<PlayerManager>();

        role = gameMaster.CmdRegisterPlayer(netID, player);

        //SkinManager SkinManager = graphics.GetComponent<SkinManager>();
        //if (SkinManager != null)
        //{
        //    if (role == "Wolf") SkinManager.ChangeToWolf();
        //    else SkinManager.ChangeToVillager();
        //} 

    }

    // Called when the object is destroyed
    void OnDisable()
    {
        if(sceneCamera != null) sceneCamera.gameObject.SetActive(true);

        gameMaster.CmdUnregisterPlayer(transform.name);
    }
}

using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerManager))]

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;

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
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();

        PlayerManager player = GetComponent<PlayerManager>();

        role = gameMaster.RegisterPlayer(netID, player);
    }

    // Called when the object is destroyed
    void OnDisable()
    {
        if(sceneCamera != null) sceneCamera.gameObject.SetActive(true);

        gameMaster.UnregisterPlayer(transform.name);
    }
}

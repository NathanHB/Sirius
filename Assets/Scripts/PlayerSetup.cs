using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    private Camera sceneCamera;
    [SerializeField]private Camera playerCam ;
    
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

    // Called when the object is destroyed
    void OnDisable()
    {
        if(sceneCamera != null) sceneCamera.gameObject.SetActive(true);
    }
}

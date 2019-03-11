using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    //[SerializeField]
    //private Behaviour[] componenentsToDisable;
   
    private Camera sceneCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        // if i'm not the local player i do not have control over certain things
        // mainly the control of the others player and camera
        if(hasAuthority) 
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null) sceneCamera.gameObject.SetActive(false);
        }
    }

    // Called when the object is destroyed
    void OnDisable()
    {
        if(sceneCamera != null) sceneCamera.gameObject.SetActive(true);
    }
}

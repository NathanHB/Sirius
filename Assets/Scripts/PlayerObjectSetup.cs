using UnityEngine;
using UnityEngine.Networking;

public class PlayerObjectSetup : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // check if the player object is mine 
        if (!isLocalPlayer) return;

        Debug.Log("spawning my thing");
        Debug.Log("Local player: " + isLocalPlayer);
        CmdSpawnUnit();
    }

    // is used to spawn unit on the server
    [Command]
    void CmdSpawnUnit()
    {
        GameObject go = Instantiate(playerPrefab);
        // propagate to all clients
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}

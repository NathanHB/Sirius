using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class gameMaster : NetworkBehaviour
{
    private const string playerNamePrefix = "player ";
    
    private static Dictionary<string, (PlayerManager, string)> players = new Dictionary<string, (PlayerManager, string)>();

    [SerializeField] private static int wolfneeded = 2;
    private static int wolfCount = 0;



    public static string CmdRegisterPlayer(string netID, PlayerManager player)
    {
        string playerID = playerNamePrefix + netID;
        
        string role = ChooseRole();
        
        players.Add(playerID, (player, role));

        player.transform.name = playerID;
        
        return role;
    }

    
    public static void CmdUnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }


    public static PlayerManager GetPlayer(string playerID)
    {
        return players[playerID].Item1;
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (var player in players)
        {
            GUILayout.Label(player.Key + " " + player.Value.Item1.transform.name + " " + player.Value.Item2);
        }
        
        GUILayout.EndVertical();
        GUILayout.EndArea();
        
    }

    public static string ChooseRole()
    {
        if (wolfCount == wolfneeded)
        {
            // role will be Villager
            return "Villager";
        }
        else
        {
            // We have a chance to be a wolf
            wolfCount++;
            return "Werewolf";
        }
    }
    
     
}

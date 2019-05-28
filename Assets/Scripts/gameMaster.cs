using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Random = System.Random;

public class gameMaster : NetworkBehaviour
{
    private const string playerNamePrefix = "player ";
    
    private static Dictionary<string, (PlayerManager, string)> players = new Dictionary<string, (PlayerManager, string)>();

    [SerializeField] private static int wolfneeded = 2;
    private static int playersNeeded = 2;
    private static int wolfCount = 0;

    public static void CmdRegisterPlayer(string netID, PlayerManager player)
    {
        string playerID = playerNamePrefix + netID;
        
        string role = ChooseRole();
        
        players.Add(playerID, (player, role));

        player.transform.name = playerID;
    }

    
    public static void CmdUnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }


    public static PlayerManager GetPlayer(string playerID)
    {
        return players[playerID].Item1;
    }

    public static string getRole(string playerId)
    {
        foreach (var elt in players)
        {
            if (elt.Key == playerId)
                return elt.Value.Item2;
        }

        return "";
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 200, 500));
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
        
            Random rnd = new Random();
            int choose = rnd.Next(0, 2);
            // We have a chance to be a wolf

            if (choose == 0)
            {
                wolfCount++;
                return "Werewolf";
            }

            return "Villager";
    }

    public static bool allPlayersConnected()
    {
        return players.Count == playersNeeded;
    }
    
     
}

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

    [SerializeField] private static int wolfneeded = 1;
    private static int playersNeeded = 2;
    private static int wolfCount = 0;
    private static bool isOver = false;
    private static string winner;


    public static void CmdRegisterPlayer(string netID, PlayerManager player)
    {
        string playerID = playerNamePrefix + netID;

        if (players.ContainsKey(playerID))
            return;
        
        string role = ChooseRole();
        
        players.Add(playerID, (player, role));

        player.transform.name = playerID;
        player.tag = role;
    }


    private void Update()
    {
        (isOver, winner) = checkIsOver();
    }

    public static (bool, string) getIsOver()
    {
        return (isOver, winner);
    }


    public static void CmdUnregisterPlayer(string playerID)
    {
        players.Remove(playerID);
    }
    

    public static int getPlayersNumber()
    {
        return players.Count;
    }


    public static string[] getPlayersId()
    {
        string[] res = new string[players.Count];
        int count = 0;
        foreach (var elt in players)
        {
            res[count]=elt.Key+"=>"+elt.Value.Item2;
            count++;
        }

        return res;
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

        return "Untagged";
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
        if (wolfCount == wolfneeded)// role will be Villager
            return "Villager";

        if (playersNeeded-players.Count<=wolfneeded)
            return "Werewolf";
        
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

    private static (bool, string) checkIsOver()
    {
        bool onlyVillagers = true,
            onlyWerewolves = true;

        foreach (var player in players)
        {
            if (player.Value.Item2 == "Villager")
                onlyWerewolves = false;
            else
                onlyVillagers = false;
        }

        if (onlyVillagers)
            return (true, "Villager");
        if(onlyWerewolves)
            return (true, "Werewolf");

        return (false, "");
    }
    
    
    
     
}

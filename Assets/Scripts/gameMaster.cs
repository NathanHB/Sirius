﻿using System;
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
    
    private static int[] votes = new int[getPlayersNumber()]; // We make a list in which the
                                                                           // index is the player index and the value the number of votes for him

    [SerializeField] private static int wolfneeded = 1;
    private static int playersNeeded = 2;
    private static int wolfCount = 0;
    private static bool isOver = false;
    private static string winner;


    public static void cmdVote(int playerIndex)
    {
        if (playerIndex == -1) return;
        votes[playerIndex] += 1;
    }

    public static void cmdEndVote()
    {
        int indexMax = 0;
        int max = 0;

        for (int i = 0; i < votes.Length; i++)
        {
            if (votes[i] > max)
            {
                max = votes[i];
                indexMax = i;
            }
        }

        string player = playerNamePrefix + (indexMax + 7);
        CmdUnregisterPlayer(player);
        GetPlayer(player).RpcTakeDmg(999);
    }


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

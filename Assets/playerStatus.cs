using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class playerStatus
{
    public static int playerExpBeforeStartGame = 0;
    public static int playerExpCurrentGame = 0;
    public static int playerExp = 0;
    public static int playerLvl = 1;
    public static int playerLvlBeforeStartGame = 0;

    public static int perkPoint = 0;



    //speed perks
    public static float schnellerNachladenPerk = 0.0f;
    public static int schnellerNachladenPerkLvl = 0;



    public static float schnellerRennenPerk = 0.0f;
    public static int schnellerRennenPerkLvl = 0;



    public static void updatePlayerLvl()
    {
        int currExp = playerExp + playerExpCurrentGame;
        while (currExp >= 1000 * playerLvl)
        {
            currExp = currExp - 1000 * playerLvl;
            perkPoint++;
            playerLvl++;
            Debug.Log("level up");
        }
        playerExp = currExp;
    }

    public static void reloadUpgrade()
    {
        if (perkPoint > 0)
        {
            schnellerNachladenPerk += 0.10f;
            schnellerNachladenPerkLvl++;
            perkPoint--;
        }
    }

    public static void runUpgrade()
    {
        if (perkPoint > 0)
        {
            schnellerRennenPerk += 0.15f;
            schnellerRennenPerkLvl++;
            perkPoint--;

        }
    }
}

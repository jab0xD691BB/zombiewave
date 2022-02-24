using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class Statistik
{

    public static int highestWave = 0;
    public static int monsterKills = 0;

    [System.Serializable]
    public class SaveStatistik
    {
        public int highestWave = 0;
        public int monsterkills = 0;

        public int playerExp = 0;
        public int playerLvl = 0;
        public int perkPoint = 0;

        public int schnellerNachladenPerkLvl = 0;
        public int schnellerRennenPerkLvl = 0;

        public SaveStatistik()
        {

        }

        public SaveStatistik(int highWave, int mk, int pe, int pl, int pp, int snp, int srp)
        {
            highestWave = highWave;
            monsterkills = mk;

            playerExp = pe;
            playerLvl = pl;
            perkPoint = pp;

            schnellerNachladenPerkLvl = snp;
            schnellerRennenPerkLvl = srp;
        }

    }

    public static void save()
    {
        string path = Application.persistentDataPath + "/stats.bin";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log("speichere: " + highestWave + " und " + monsterKills);
        SaveStatistik data = new SaveStatistik(highestWave, monsterKills, playerStatus.playerExp, playerStatus.playerLvl, playerStatus.perkPoint, playerStatus.schnellerNachladenPerkLvl, playerStatus.schnellerRennenPerkLvl);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static void load()
    {
        SaveStatistik data = new SaveStatistik();
        data = loadStatistik();



        highestWave = data.highestWave;
        monsterKills = data.monsterkills;

        playerStatus.playerExp = data.playerExp;
        playerStatus.playerLvl = data.playerLvl;
        playerStatus.perkPoint = data.perkPoint;

        playerStatus.schnellerNachladenPerkLvl = data.schnellerNachladenPerkLvl;
        playerStatus.schnellerNachladenPerk = data.schnellerNachladenPerkLvl * 0.10f;

        playerStatus.schnellerRennenPerkLvl = data.schnellerRennenPerkLvl;
        playerStatus.schnellerRennenPerk = data.schnellerRennenPerkLvl * 0.15f;

        Debug.Log("lade: " + highestWave + " und " + monsterKills);

    }

    public static void resetAndSave()
    {
        string path = Application.persistentDataPath + "/stats.bin";

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);
        Debug.Log("speichere: " + highestWave + " und " + monsterKills);
        SaveStatistik data = new SaveStatistik(0, 0, 0, 0, 0, 0, 0);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static SaveStatistik loadStatistik()
    {
        string path = Application.persistentDataPath + "/stats.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveStatistik data = formatter.Deserialize(stream) as SaveStatistik;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found " + path);
        }

        return null;
    }

    public static void setHighestWave(int round)
    {
        if (highestWave < round)
        {
            highestWave = round;
        }
    }

}

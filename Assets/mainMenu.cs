using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{

    public GameObject loadingScreen;
    public GameObject perkScreen;

    public Slider slider;
    public Text progressText;
    public AudioClip clickSound;
    AudioSource source;

    Text highestWaveCounter;
    Text monsterKillsCounter;

    Text playerLevelTxt;
    Text perkPointsTxt;
    Text expTxt;

    private void Awake()
    {

        gameObject.AddComponent<AudioSource>();

        source = gameObject.GetComponent<AudioSource>();
        source.clip = clickSound;

        highestWaveCounter = GameObject.Find("highestWaveCounter").GetComponent<Text>();
        monsterKillsCounter = GameObject.Find("monsterKillsCounter").GetComponent<Text>();

        playerLevelTxt = GameObject.Find("lvlText").GetComponent<Text>();
        perkPointsTxt = GameObject.Find("perkPoint").GetComponent<Text>();
        expTxt = GameObject.Find("exp").GetComponent<Text>();



        Statistik.load();
        Debug.Log("lade: " + Statistik.highestWave + " und " + Statistik.highestWave);

        highestWaveCounter.text = Statistik.highestWave.ToString();
        monsterKillsCounter.text = Statistik.monsterKills.ToString();

        //playerStatus.perkPoint = 100;
        playerLevelTxt.text = "lvl " + playerStatus.playerLvl.ToString();
        perkPointsTxt.text = playerStatus.perkPoint.ToString();
        expTxt.text = playerStatus.playerExp + "/" + playerStatus.playerLvl * 1000;

        Debug.Log("playerexp " + playerStatus.playerExp);
    }



    public void PlayGame()
    {
        source.Play();
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously("game"));
    }


    // TODO: anders aktualisieren wenn perkscreen aktiviert wird
    public void openPerk()
    {
        perkScreen.SetActive(true);
        GameObject.Find("reloadPerkLvlText").GetComponent<Text>().text = "lvl " + playerStatus.schnellerNachladenPerkLvl;
        GameObject.Find("runPerkLvlText").GetComponent<Text>().text = "lvl " + playerStatus.schnellerRennenPerkLvl;

    }

    public void backMenu()
    {
        perkScreen.SetActive(false);
    }

    public void upgradePerk(string perkName)
    {
        switch (perkName)
        {
            case "reload":
                playerStatus.reloadUpgrade();
                GameObject.Find("reloadPerkLvlText").GetComponent<Text>().text = "lvl " + playerStatus.schnellerNachladenPerkLvl;
                break;
            case "run":
                playerStatus.runUpgrade();
                GameObject.Find("runPerkLvlText").GetComponent<Text>().text = "lvl " + playerStatus.schnellerRennenPerkLvl;
                break;
            default:
                break;
        }

        perkPointsTxt.text = playerStatus.perkPoint.ToString();

    }

    IEnumerator LoadAsynchronously(string scene)
    {

        yield return new WaitForSeconds(0.5f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);


        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;
            progressText.text = progress * 100 + "%";

            yield return null;
        }



    }


    public void resetStats()
    {
        Statistik.resetAndSave();
        Statistik.load();

        highestWaveCounter.text = Statistik.highestWave.ToString();
        monsterKillsCounter.text = Statistik.monsterKills.ToString();

        playerLevelTxt.text = "lvl " + playerStatus.playerLvl.ToString();
        perkPointsTxt.text = playerStatus.perkPoint.ToString();
        expTxt.text = playerStatus.playerExp + "/" + playerStatus.playerLvl * 1000;
    }


    
    //Statistik.save();

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("pause: " + pause);
            Statistik.save();
            
        }
        else
        {
            Debug.Log("pause: " + pause);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class showStatistik : MonoBehaviour
{

    public Slider slider;
    public Text sliderText;
    public Text lvlText;


    float progressBarValue = 0f;
    float lerpSpeed = 0.25f;
    float t = 0f;

    float simulatePlayerExpStart = 0f;
    float simulatePlayerExpEnd = 0f;

    float simulateLvl = 0f;

    float expMaxValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerStatus.playerExp);
        simulateLvl = playerStatus.playerLvlBeforeStartGame;

        simulatePlayerExpStart = playerStatus.playerExpBeforeStartGame;
        simulatePlayerExpEnd = playerStatus.playerExpBeforeStartGame + playerStatus.playerExpCurrentGame;

        if (simulateLvl == 0)
        {
            simulateLvl = 1;
        }

        slider.maxValue = simulateLvl * 1000;

        slider.value = simulatePlayerExpStart;

        sliderText.text = simulatePlayerExpStart + "/" + simulateLvl * 1000;


        lvlText.text = simulateLvl.ToString();



    }



    // Update is called once per frame
    void Update()
    {
        progressBarValue = Mathf.Lerp(simulatePlayerExpStart, simulatePlayerExpEnd, t);
        Debug.Log(progressBarValue);
        slider.value = progressBarValue;

        sliderText.text = Mathf.RoundToInt(progressBarValue) + "/" + simulateLvl * 1000;

        if (slider.maxValue <= progressBarValue)
        {
            simulatePlayerExpEnd -= (simulateLvl * 1000);

            simulateLvl++;
            lvlText.text = simulateLvl.ToString();

            slider.maxValue = simulateLvl * 1000;

            simulatePlayerExpStart = 0;
        }

        t += Time.deltaTime * lerpSpeed;
    }

    IEnumerator loadExp()
    {



        yield return new WaitForSeconds(0.1f);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("menu");
    }

}

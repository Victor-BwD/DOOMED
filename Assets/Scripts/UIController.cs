using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private PlayerController playerController;
    public Slider sliderLifePlayer;
    public GameObject gameOverPanel;
    public Text timingSurviveText;
    public Text textTimingSurvivedMax; // Text in unity editor to show max time
    private float timePointsSaved; // var to save the best time

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sliderLifePlayer.maxValue = playerController.statusPlayer.health;
        UpdateSliderPlayerLife();
        Time.timeScale = 1;
        timePointsSaved = PlayerPrefs.GetFloat("MaxPoints");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSliderPlayerLife()
    {
        sliderLifePlayer.value = playerController.statusPlayer.health;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

        int minutes = (int)(Time.timeSinceLevelLoad / 60); // convert to int the time 
        int seconds = (int)(Time.timeSinceLevelLoad % 60);
        timingSurviveText.text = "You survived for " + minutes + " minutes and " + seconds + " seconds.";

        AdjustMaxPoints(minutes, seconds);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    void AdjustMaxPoints(int min, int sec)
    {
        if(Time.timeSinceLevelLoad > timePointsSaved)
        {
            timePointsSaved = Time.timeSinceLevelLoad;
            textTimingSurvivedMax.text = string.Format("Your best time: {0} minutes and {1} seconds", min, sec);
            PlayerPrefs.SetFloat("MaxPoints", timePointsSaved);
        }
        if (textTimingSurvivedMax.text == "")
        {
            min = (int)timePointsSaved / 60;
            sec = (int)timePointsSaved % 60;
            textTimingSurvivedMax.text = string.Format("Your best time: {0} minutes and {1} seconds", min, sec);
        }
    }
}

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
    private int numberOfDeadZombies;
    public Text textNumberOfDeadZombies; 

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

    public void AttNumberDeadZombie() // method to call in enemyController to make score
    {
        numberOfDeadZombies++; // increment variable
        textNumberOfDeadZombies.text = string.Format("x{0}", numberOfDeadZombies); // show in the UI the text and points
    }

    public void UpdateSliderPlayerLife()
    {
        sliderLifePlayer.value = playerController.statusPlayer.health; // update slider based on player health
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;

        int minutes = (int)(Time.timeSinceLevelLoad / 60); // convert to int the time 
        int seconds = (int)(Time.timeSinceLevelLoad % 60);  // convert to int the time 
        timingSurviveText.text = "You survived for " + minutes + " minutes and " + seconds + " seconds.";

        AdjustMaxPoints(minutes, seconds);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    void AdjustMaxPoints(int min, int sec)
    {
        if(Time.timeSinceLevelLoad > timePointsSaved) // if the time since level is load are bigger than time saved
        {
            timePointsSaved = Time.timeSinceLevelLoad; // keep this in the variable
            textTimingSurvivedMax.text = string.Format("Your best time: {0} minutes and {1} seconds", min, sec); // concat
            PlayerPrefs.SetFloat("MaxPoints", timePointsSaved);
        }
        if (textTimingSurvivedMax.text == "") // if text is empty
        {
            min = (int)timePointsSaved / 60;
            sec = (int)timePointsSaved % 60;
            textTimingSurvivedMax.text = string.Format("Your best time: {0} minutes and {1} seconds", min, sec);
        }
    }
}

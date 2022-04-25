using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerController playerController;
    public Slider sliderLifePlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sliderLifePlayer.maxValue = playerController.statusPlayer.health;
        UpdateSliderPlayerLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSliderPlayerLife()
    {
        sliderLifePlayer.value = playerController.statusPlayer.health;
    }
}

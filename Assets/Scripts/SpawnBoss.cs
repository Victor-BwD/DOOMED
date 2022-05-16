using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    private float timeToNextGeneration = 0;
    public float timeBetweenGenerations = 60;
    public GameObject bossPrefab;
    private UIController scriptUIController;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextGeneration = timeBetweenGenerations;
        scriptUIController = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > timeToNextGeneration)
        {
            Instantiate(bossPrefab, transform.position, Quaternion.identity);
            scriptUIController.AppearTextBossCreated();
            timeToNextGeneration = Time.timeSinceLevelLoad + timeBetweenGenerations;
        }
    }
}

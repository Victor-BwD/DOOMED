using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    private float timeToNextGeneration = 0;
    public float timeBetweenGenerations = 60;
    public GameObject bossPrefab;
    private UIController scriptUIController;
    public Transform[] possiblePositionsToSpawn;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextGeneration = timeBetweenGenerations;
        scriptUIController = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad > timeToNextGeneration)
        {
            Vector3 SpawnPosition = CalculeMoreFarDistanceFromThePlayer();
            Instantiate(bossPrefab, SpawnPosition, Quaternion.identity); // spawn boss in one of spawn location
            scriptUIController.AppearTextBossCreated();
            timeToNextGeneration = Time.timeSinceLevelLoad + timeBetweenGenerations;
        }
    }

    // Function to calculate the greatest distance between the player and the spawner
    Vector3 CalculeMoreFarDistanceFromThePlayer()
    {
        Vector3 GreatestDistancePosition = Vector3.zero;
        float GreatestDistanceFounded = 0;
        foreach(Transform p in possiblePositionsToSpawn)
        {
            float distanceBetweenPlayer = Vector3.Distance(p.position, player.position);
            if(distanceBetweenPlayer > GreatestDistanceFounded)
            {
                GreatestDistanceFounded = distanceBetweenPlayer;
                GreatestDistancePosition = p.position;
            }
        }

        return GreatestDistancePosition;
    }
}

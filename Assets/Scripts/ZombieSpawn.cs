using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject zombiePrefab;
    float timeCounting = 0;

    public float timeSpawnZombies = 1;

    public LayerMask LayerZumbi;

    private float spawnDistance = 3f;

    public float distancePlayerToSpawnEnemies = 20f;

    private GameObject player;

    private int maxNumberZombiesAlive = 3;

    private int numberZombiesAlive;

    private float proxTimeToIncreaseDifficulty = 20; // each 30 seconds increase dificulty

    private float countIncreaseDifficulty; // keep the time to increase difficulty

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        countIncreaseDifficulty = proxTimeToIncreaseDifficulty;
        for(int i = 0; i < maxNumberZombiesAlive; i++) // start the phase with maximum zombies alive
        {
            StartCoroutine(SpawNewZombie()); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool canIMakeZombiesFromTheDistance = Vector3.Distance(transform.position, player.transform.position) > distancePlayerToSpawnEnemies; // check if player are far from spawn point

        if (canIMakeZombiesFromTheDistance && numberZombiesAlive < maxNumberZombiesAlive) // check if player are far from spawn point and check if number of zumbis alive is minus than max number of zombies allowed
        {
            timeCounting += Time.deltaTime; // Timer couting 1 sec

            if (timeCounting >= timeSpawnZombies)
            {
                StartCoroutine(SpawNewZombie());
                timeCounting = 0;
            }
        }

        if(Time.timeSinceLevelLoad >= countIncreaseDifficulty)
        {
            maxNumberZombiesAlive++;
            countIncreaseDifficulty = Time.timeSinceLevelLoad + proxTimeToIncreaseDifficulty;
        }

        
    }

    // spawn
    IEnumerator SpawNewZombie()
    {
        Vector3 createPosition = RandomizerPosition();
        Collider[] collisions = Physics.OverlapSphere(createPosition, 1, LayerZumbi); // Create a ball to check if have a collision in this place to spawn zombie safe

       
        while (collisions.Length > 0)
        {
            createPosition = RandomizerPosition();
            collisions = Physics.OverlapSphere(createPosition, 1, LayerZumbi); // Create a ball to check if have a collision in this place to spawn zombie safe
            yield return null; // make certain dont have a infinite loop and crash Unity
        }
        EnemyController zombie = Instantiate(zombiePrefab, createPosition, this.transform.rotation).GetComponent<EnemyController>(); // Instantiate gets the vector3 of RandomizerPosition and get the script
        zombie.mySpawn = this; // spawn that spawned the zombie, link zombie a spawn
        numberZombiesAlive++;
    }

    // Increse the radius of the spawns
    Vector3 RandomizerPosition()
    {
        Vector3 position = Random.insideUnitSphere * spawnDistance;
        position += transform.position;
        position.y = 0;

        return position;
    }

    public void DecreaseAmountZombiesAlive()
    {
        numberZombiesAlive--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnDistance);
    }
}

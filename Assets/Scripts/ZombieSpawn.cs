using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject zombie;
    float timeCounting = 0;

    public float timeSpawnZombies = 1;

    public LayerMask LayerZumbi;

    private float spawnDistance = 3f;

    public float distancePlayerToSpawnEnemies = 20f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > distancePlayerToSpawnEnemies) // check if player are far from spawn point
        {
            timeCounting += Time.deltaTime; // Timer couting 1 sec

            if (timeCounting >= timeSpawnZombies)
            {
                StartCoroutine(SpawNewZombie());
                timeCounting = 0;
            }
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
        Instantiate(zombie, createPosition, this.transform.rotation); // Instantiate gets the vector3 of RandomizerPosition
    }

    // Increse the radius of the spawns
    Vector3 RandomizerPosition()
    {
        Vector3 position = Random.insideUnitSphere * spawnDistance;
        position += transform.position;
        position.y = 0;

        return position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnDistance);
    }
}

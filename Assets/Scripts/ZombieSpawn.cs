using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject zombie;
    float timeCounting = 0;

    public float timeSpawnZombies = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounting += Time.deltaTime; // Timer couting 1 sec

        if(timeCounting >= timeSpawnZombies)
        {
            Instantiate(zombie, this.transform.position, this.transform.rotation);
            timeCounting = 0;
        }

        
    }
}

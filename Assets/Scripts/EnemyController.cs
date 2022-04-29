using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IKilliable
{
    
    public GameObject player;
    Animator anim;
    private Moviment movimentEnemy;
    private AnimationCaracters animationEnemy;
    private Status statusEnemy;
    public AudioClip deathSoundZombie;
    private Vector3 randomPosition;
    private Vector3 direction;
    private float countTimingWander;
    private float timingbetweenRandomPositions = 4f;
    private float radiusSpawnDistance = 12;
    private float percentGenerateMedkit = 0.1f;
    public GameObject MedkitPrefab;
    private UIController scriptUIController;
    [HideInInspector]
    public ZombieSpawn mySpawn;
    

    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.FindWithTag("Player");
        movimentEnemy = GetComponent<Moviment>();
        animationEnemy = GetComponent<AnimationCaracters>();
        statusEnemy = GetComponent<Status>();
        scriptUIController = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        RandomEnemyGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        movimentEnemy.RotationCaracter(direction);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        animationEnemy.AnimationMoving(direction.magnitude);

        if(distance > 15)
        {
            Wander();
        }
        else if(distance > 2.5)
        {

            Pursuit();
            animationEnemy.Attack(false);

        }
        else
        {
            direction = player.transform.position - transform.position;
            animationEnemy.Attack(true);
        }

    }
    // Make zombie wander through the local
    void Wander()
    {
        countTimingWander -= Time.deltaTime;
        if(countTimingWander <= 0)
        {
            randomPosition = RandomPosition();
            countTimingWander += timingbetweenRandomPositions + Random.Range(-1f, 1f); // make zombies walk
        }

        bool closeEnough = Vector3.Distance(transform.position, randomPosition) <= 0.05; // return true or false

        if (closeEnough == false)
        {
            direction = randomPosition - transform.position;
            movimentEnemy.MovimentCaracter(direction, statusEnemy.speed);
        }
        
        
    }

    // get a random position to zombie
    Vector3 RandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * radiusSpawnDistance; // generate a number inside of the area of spawn and increse the area from multiply
        position += transform.position;
        position.y = transform.position.y;

        return position;
    }

    void HitingPlayer()
    {
        int damageRandom = Random.Range(20, 30);
        player.GetComponent<PlayerController>().TakeDamage(damageRandom);
    }

    void RandomEnemyGeneration()
    {
        int zombieTypeGenerator = Random.Range(1, 28); // Random int between 1 and 27
        transform.GetChild(zombieTypeGenerator).gameObject.SetActive(true); // Enter on zombie, get child, return to gameobject and active
    }

    void Pursuit()
    {
        direction = player.transform.position - transform.position; // pursuit the player
        movimentEnemy.MovimentCaracter(direction, statusEnemy.speed);
    }

    public void TakeDamage(int dano)
    {
        statusEnemy.health -= dano;
        if(statusEnemy.health <= 0)
        {
            Die();
        }
    }

    public void Die() // method to call when zombie dies
    {
        Destroy(gameObject);
        AudioController.instance.PlayOneShot(deathSoundZombie);
        VerifyGenerationMedKit(percentGenerateMedkit);
        scriptUIController.AttNumberDeadZombie(); // Update quantity of dead zombies
        mySpawn.DecreaseAmountZombiesAlive();
    }

    void VerifyGenerationMedKit(float percentGeneration) // Method to check if create medkit or not
    {
        if(Random.value < percentGeneration)
        {
            Instantiate(MedkitPrefab, transform.position, Quaternion.identity);
        }
    }
}

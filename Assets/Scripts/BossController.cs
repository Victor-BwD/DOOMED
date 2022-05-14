using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IKilliable
{
    private Transform player;
    private NavMeshAgent agent;
    private Status statusBoss;
    private AnimationCaracters animationBoss;
    private Moviment rotationBoss;
    public GameObject medkitPrefab;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // Receive the player object with transform
        statusBoss = GetComponent<Status>();
        agent.speed = statusBoss.speed;
        animationBoss = GetComponent<AnimationCaracters>();
        rotationBoss = GetComponent<Moviment>();
    }

    private void Update()
    {
        agent.SetDestination(player.position);
        animationBoss.AnimationMoving(agent.velocity.magnitude);

        if (agent.hasPath)
        {
            bool imCloseToThePlayer = agent.remainingDistance <= agent.stoppingDistance;

            if (imCloseToThePlayer)
            {
                animationBoss.Attack(true);
                Vector3 direction = player.position - transform.position;
                rotationBoss.RotationCaracter(direction);
            }
            else
            {
                animationBoss.Attack(false);
            }
        }
    }

    void HitingPlayer()
    {
        int damageRandom = Random.Range(30, 40);
        player.GetComponent<PlayerController>().TakeDamage(damageRandom);
    }

    public void TakeDamage(int dano)
    {
        statusBoss.health -= dano;
        if(statusBoss.health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animationBoss.Dead();
        rotationBoss.Death();
        this.enabled = false;
        agent.enabled = false;
        Instantiate(medkitPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject, 2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float speedBullet = 20;
    public AudioClip deathSoundZombie;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speedBullet * Time.deltaTime);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Quaternion oppositePositionFromBullet = Quaternion.LookRotation(-transform.forward);
        switch(other.tag)
        {
            case "Inimigo":
                EnemyController enemy = other.GetComponent<EnemyController>();
                enemy.TakeDamage(1);
                enemy.BloodParticle(transform.position, oppositePositionFromBullet);
                break;
            case "Boss":
                other.GetComponent<BossController>().TakeDamage(1);
                break ;
        }

        Destroy(this.gameObject);
        
    }
}

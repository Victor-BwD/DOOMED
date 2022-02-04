using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject player;
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        int zombieTypeGenerator = Random.Range(1, 28); // Random int between 1 and 27
        transform.GetChild(zombieTypeGenerator).gameObject.SetActive(true); // Enter on zombie, get child, return to gameobject and active
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;

        Quaternion newRotation = Quaternion.LookRotation(direction);

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance > 2.5)
        {
            
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

           
            rb.MoveRotation(newRotation);

            GetComponent<Animator>().SetBool("isAttacking", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("isAttacking", true);
        }

    }

    void HitingPlayer()
    {
        Time.timeScale = 0;
        player.GetComponent<PlayerMoviment>().gameOverText.SetActive(true);
        player.GetComponent<PlayerMoviment>().isAlive = false;
    }
}

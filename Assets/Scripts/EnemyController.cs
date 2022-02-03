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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance > 2.5)
        {
            Vector3 direction = player.transform.position - transform.position;
            rb.MovePosition(rb.position + direction.normalized * speed * Time.deltaTime);

            Quaternion newRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(newRotation);
        }
    }
}

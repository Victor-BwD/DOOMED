using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float speedBullet = 20;

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
        if(other.tag == "Inimigo")
        {
            Destroy(other.gameObject);
        }

        Destroy(this.gameObject);
        
    }
}

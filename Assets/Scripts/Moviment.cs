using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moviment : MonoBehaviour
{
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovimentCaracter(Vector3 direction, float speed)
    {
        myRigidbody.MovePosition(myRigidbody.position + direction.normalized * speed * Time.deltaTime);
    }

    public void RotationCaracter(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(newRotation);
    }

    public void Death()
    {
        myRigidbody.constraints = RigidbodyConstraints.None;
        myRigidbody.velocity = Vector3.zero;
        GetComponent<Collider>().enabled = false;
    }
}

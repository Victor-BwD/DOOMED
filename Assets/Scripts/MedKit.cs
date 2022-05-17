using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{

    private int healAmount = 25;
    private int selfDestructionTime = 5;

    private void Start()
    {
        Destroy(gameObject, selfDestructionTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().HealHealth(healAmount);
            Destroy(gameObject);
        }
    }
}

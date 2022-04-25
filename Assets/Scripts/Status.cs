using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int health_Begin = 100;
    [HideInInspector]
    public int health;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        health = health_Begin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

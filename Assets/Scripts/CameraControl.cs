using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    private Vector3 distancePlayer;

    // Start is called before the first frame update
    void Start()
    {
        distancePlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        transform.position = player.transform.position + distancePlayer;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public GameObject player;
    public Vector2 playerOffset;

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = player.transform.position + new Vector3(playerOffset.x, playerOffset.y, player.transform.position.z);
    }
}

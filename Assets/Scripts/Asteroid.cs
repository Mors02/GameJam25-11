using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float force = 50f;       // Force applied to the projectile
    public float destroyTime = 20f; // Time in seconds before the projectile is destroyed

    
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized * force, ForceMode.Impulse);
        }

        Destroy(gameObject, destroyTime);
    }
}
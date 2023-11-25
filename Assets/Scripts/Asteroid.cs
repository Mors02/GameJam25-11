using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float force = 50f;       // Force applied to the projectile
    public float destroyTime = 20f; // Time in seconds before the projectile is destroyed
    public GameObject asteroidExplosion;
    
    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            GetComponent<Rigidbody>().AddForce(directionToPlayer.normalized * force, ForceMode.Impulse);
        }

        InvokeRepeating("Destroy", 30f, 10f);

    }

    private void Destroy()
    {


        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(asteroidExplosion, transform.position, transform.rotation);
    }
}
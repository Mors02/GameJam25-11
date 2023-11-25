using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(collision.gameObject);
        }
    }

}

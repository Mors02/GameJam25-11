using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;

            //activate script on player to choose power ups--------
            print("power up");

            Destroy(this.gameObject);
        }
    }
}

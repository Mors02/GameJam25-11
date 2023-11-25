using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject player;

    public UIManager ui;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;

            ui.Pause();

            Destroy(this.gameObject);
        }
    }
}

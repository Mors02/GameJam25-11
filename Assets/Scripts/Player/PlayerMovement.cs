using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  
    public float moveSpeed = 10f;
    public Rigidbody rb;
    Vector3 movement;
    [SerializeField]
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        SpawnManager.i.NewRound();
        this.moveSpeed = stats.GetStat(PowerUpTypes.movementSpeed);
    }

    public void UpdateMovement()
    {
        this.moveSpeed = stats.GetStat(PowerUpTypes.movementSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Mathf.Round(Input.GetAxis("Horizontal") * 100f) / 100f;
        movement.y = Mathf.Round(Input.GetAxis("Vertical") * 100f) / 100f;
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 1000f);
        }
    }


 /*
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float playerSpeed = 50f;
 
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void FixedUpdate()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

    }*/

}

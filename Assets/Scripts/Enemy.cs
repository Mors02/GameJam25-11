using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;

    private Transform player;

    void Start()
    {
        // Assuming your player has the "Player" tag. Adjust as needed.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Rotate towards the player
            Quaternion toRotation = Quaternion.LookRotation(directionToPlayer, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            // Move towards the player
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}

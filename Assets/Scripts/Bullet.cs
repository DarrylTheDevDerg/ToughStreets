using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Variables you can tweak in the Inspector
    [Header("Bullet Settings")]
    public float bulletSpeed = 20f;         // Speed of the bullet
    public float bulletLifetime = 5f;       // How long the bullet exists before destroying itself

    private int randomDamage;
    private Rigidbody rb;
    private PlayerStats pS;
    private Enemy en;

    void Start()
    {
        // Grab the Rigidbody component attached to the bullet
        rb = GetComponent<Rigidbody>();

        // Launch the bullet forward
        rb.velocity = transform.forward * bulletSpeed;

        pS = FindObjectOfType<PlayerStats>();
        en = GetComponent<Enemy>();

        // Destroy the bullet after 'bulletLifetime' seconds
        Destroy(gameObject, bulletLifetime);
    }

    // This function will handle what happens when the bullet hits something
    void OnCollisionEnter(Collision collision)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerAttack pA = player.GetComponent<PlayerAttack>();

        Enemy enemy = FindObjectOfType<Enemy>();

        if (collision.gameObject.CompareTag("Player"))
        {
            pA.SetInvFrames();
            enemy.PlayerHarm();
            Destroy(gameObject);
        }

        // Just to keep it simple, we'll destroy the bullet when it hits any object
        

        // You can also add effects or damage logic here!
        // For example:
        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     // Apply damage to the enemy here
        // }

        // Optionally, instantiate some hit effects like sparks or explosions
        // Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}

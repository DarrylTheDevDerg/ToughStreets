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
    private TransparencyEffect tE;

    void Start()
    {
        // Grab the Rigidbody component attached to the bullet
        rb = GetComponent<Rigidbody>();

        // Launch the bullet forward
        rb.velocity = Vector3.zero;
        rb.velocity = transform.forward * bulletSpeed;

        pS = FindObjectOfType<PlayerStats>();
        en = GetComponent<Enemy>();

        tE = FindObjectOfType<TransparencyEffect>();

        // Destroy the bullet after 'bulletLifetime' seconds
        Destroy(gameObject, bulletLifetime);
    }

    // This function will handle what happens when the bullet hits something
    private void OnTriggerEnter(Collider other)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerAttack pA = player.GetComponent<PlayerAttack>();

        Enemy enemy = FindObjectOfType<Enemy>();

        if (other.gameObject.CompareTag("Player") && !tE.getInvulnerable())
        {
            enemy.PlayerHarm();
            Destroy(gameObject);
        }
    }
}

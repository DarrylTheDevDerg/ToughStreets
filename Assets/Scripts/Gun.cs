using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;       // The bullet prefab to spawn
    public Transform firePoint;           // The position where bullets are spawned from
    private FollowPlayer fP;
    private Transform player;

    private void Start()
    {
        fP = GetComponent<FollowPlayer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Get direction to the player but ignore Y-axis for horizontal rotation
        Vector3 direction = player.position - firePoint.position;
        direction.y = 0;  // Lock Y-axis (no tilting up or down)

        // Calculate the desired rotation
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the gun horizontally towards the player
        firePoint.rotation = Quaternion.Slerp(transform.rotation, targetRotation, fP.rotationSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        // Ensure the bullet is rotated correctly when instantiated
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Optional: Force the bullet to point forward in world space, just in case
        bullet.transform.forward = firePoint.forward;
    }
}


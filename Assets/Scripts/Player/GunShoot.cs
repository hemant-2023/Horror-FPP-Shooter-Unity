using UnityEngine;

public class GunShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public string bulletPrefabPath = "Prefabs/Bullet"; // Path inside Resources folder
    public float bulletSpeed = 20f;
    public float bulletLifetime = 5f;

    [Header("Spawn Settings")]
    public Transform bulletSpawnPoint; // Drag the BulletSpawnPoint here

    [Header("Fire Settings")]
    public float fireRate = 0.25f; // Time between shots (0.25s = 4 shots/sec)
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) // Left-click
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Reset fire timer
        }
    }

    void Shoot()
    {
        // Load the bullet prefab from Resources
        GameObject bulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;

        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Spawn bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Apply velocity
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed; // Fixed here
            }

            // Destroy bullet after lifetime
            Destroy(bullet, bulletLifetime);
        }
        else
        {
            Debug.LogWarning("Bullet prefab not found, or Bullet Spawn Point not assigned!");
        }
    }
}

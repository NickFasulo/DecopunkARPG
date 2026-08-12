using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Setup")]
    [SerializeField] private Transform firePoint;     // Drag your FirePoint object here
    [SerializeField] private GameObject bulletPrefab; // Drag your Bullet prefab here

    [Header("Weapon Stats")]
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float fireRate = 0.15f;  // Seconds between shots

    private float nextFireTime = 0f;

    private void Update()
    {
        // Left Click or Hold Left Click to shoot
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            Debug.LogWarning("PlayerShooting: FirePoint or BulletPrefab missing in Inspector!");
            return;
        }

        // Spawn bullet at FirePoint position & rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Accelerate bullet forward
        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

        // Self-destruct bullet after 2 seconds so scene doesn't get cluttered
        Destroy(bullet, 2f);
    }
}
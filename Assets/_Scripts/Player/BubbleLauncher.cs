using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLauncher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Collider2D playerCollider2D;

    [Header("Settings")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;

    private bool shouldFire;
    private float previousFireTime;

    private void HandlePrimaryFire(bool shouldFire)
    {
        this.shouldFire = shouldFire;
    }


    private void Update()
    {
        if (Time.time < (1 / fireRate) + previousFireTime) return;

        SpawnProjectile(projectileSpawnPoint.position, projectileSpawnPoint.up);

        previousFireTime = Time.time;
    }

    private void SpawnProjectile(Vector3 spawnPosition, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Physics2D.IgnoreCollision(playerCollider2D, projectileInstance.GetComponent<Collider2D>());

        projectileInstance.transform.up = direction;

        if (projectileInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * projectileSpeed;
        }
    }
}

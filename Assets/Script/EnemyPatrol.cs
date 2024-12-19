using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolRadius = 3f;       // Radius within which the enemy will patrol
    public float speed = 2f;              // Movement speed of the enemy
    private Vector2 patrolCenter;         // Center of patrol (player's initial position)
    private Vector2 targetPosition;       // Current target position for patrol
    public Transform bulletSpawnPoint;    // Reference to the bullet spawn point
    public float shootInterval = 2f;      // Interval between shots
    private float shootTimer;             // Timer to track shooting intervals

    private void Start()
    {
        patrolCenter = this.gameObject.transform.position;
        SetRandomTargetPosition();
        shootTimer = shootInterval;
    }

    private void Update()
    {
        Patrol();
        ShootAtPlayer();
    }

    private void Patrol()
    {
        // Move the enemy towards the current target position smoothly
        transform.position = Vector2.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        // If the enemy reaches the target position, pick a new random target
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        targetPosition = patrolCenter + randomDirection * patrolRadius;
    }

    private void ShootAtPlayer()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            // Reset the shoot timer
            shootTimer = shootInterval;

            // Get a bullet from the bullet pool
            GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;
                bullet.SetActive(true);

                // Add logic to make the bullet move towards the player
                bullet.GetComponent<Bullet>().ShootAtPlayer();
            }
        }
    }

    // on trigger with player disable this gameobject

   
}

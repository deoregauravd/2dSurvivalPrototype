using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;              // Speed of the bullet
    private Transform player;             // Reference to the player
    private Vector2 direction;            // Direction to move the bullet
    public int damage = 1;                // Damage dealt by the bullet

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ShootAtPlayer();
        // disable the bullet after 5 seconds
        Invoke("DisableBullet", 10f);
    }

    public void ShootAtPlayer()
    {
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
    }

    private void Update()
    {
        // Move the bullet towards the player
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Call the TakeDamage method on the IDamageable component
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            // Deactivate the bullet instead of destroying it
            gameObject.SetActive(false);
        }
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }
}
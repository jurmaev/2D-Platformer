using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        _hit = true;
        _collider.enabled = false;

        _animator.SetTrigger("explode");
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
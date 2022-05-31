using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        Hit = true;
        Collider.enabled = false;

        Animator.SetTrigger("explode");
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
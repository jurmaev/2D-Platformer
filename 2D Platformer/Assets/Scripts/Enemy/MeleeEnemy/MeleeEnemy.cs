using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float damage;
    private Health _playerHealth;

    private void Update()
    {
        CooldownTimer += Time.deltaTime;

        if (PlayerInSight() && CooldownTimer >= attackCooldown)
        {
            CooldownTimer = 0;
            Animator.SetBool("moving", false);
            Animator.SetTrigger("meleeAttack");
        }

        if (EnemyPatrol != null)
            EnemyPatrol.enabled = !PlayerInSight();
    }


    private bool PlayerInSight()
    {
        var hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
            Vector2.left, 0, playerLayer);
        if (hit.collider != null)
            _playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void DamagePlayer()
    {
        if (PlayerInSight() && _playerHealth != null)
            _playerHealth.TakeDamage(damage);
    }
}
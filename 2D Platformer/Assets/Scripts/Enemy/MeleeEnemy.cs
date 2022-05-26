using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float damage;
    private Health _playerHealth;

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && _cooldownTimer >= attackCooldown)
        {
            _cooldownTimer = 0;
            _animator.SetBool("moving", false);
            _animator.SetTrigger("meleeAttack");
        }

        if (_enemyPatrol != null)
            _enemyPatrol.enabled = !PlayerInSight();
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
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")] [SerializeField]
    private Transform firepoint;

    [SerializeField] private GameObject[] fireballs;
    private void Update()
    {
        _cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && _cooldownTimer >= attackCooldown)
        {
            _cooldownTimer = 0;
            _animator.SetBool("moving", false);
            _animator.SetTrigger("rangedAttack");
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
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void RangedAttack()
    {
        _cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (var i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }

        return 0;
    }
}
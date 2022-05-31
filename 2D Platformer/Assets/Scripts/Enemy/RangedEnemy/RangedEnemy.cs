using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")] [SerializeField]
    private Transform firepoint;

    [SerializeField] private GameObject[] fireballs;
    private void Update()
    {
        CooldownTimer += Time.deltaTime;

        if (PlayerInSight() && CooldownTimer >= attackCooldown)
        {
            CooldownTimer = 0;
            Animator.SetBool("moving", false);
            Animator.SetTrigger("rangedAttack");
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
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    private void RangedAttack()
    {
        CooldownTimer = 0;
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
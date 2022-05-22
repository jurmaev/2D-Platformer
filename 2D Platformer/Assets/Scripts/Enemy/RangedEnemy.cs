using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    
    private float _cooldownTimer = Mathf.Infinity;
    private Animator _animator;
    private EnemyPatrol _enemyPatrol;

    [Header("Ranged Attack")] [SerializeField]
    private Transform firepoint;

    [SerializeField] private GameObject[] fireballs;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        Physics2D.IgnoreCollision(boxCollider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }
    
   
    
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
        var hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}

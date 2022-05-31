using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float range;
    [Header("Collider Parameters")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] protected LayerMask playerLayer;
    protected float CooldownTimer = Mathf.Infinity;
    protected Animator Animator;
    protected EnemyPatrol EnemyPatrol;
    
    protected virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        EnemyPatrol = GetComponentInParent<EnemyPatrol>();
       Physics2D.IgnoreCollision(boxCollider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
       
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    
}

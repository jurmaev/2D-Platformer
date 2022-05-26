using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float meleeDamage;

    [SerializeField] private float attackRate;
    public Mana playerMana { get; private set; }
    [SerializeField] private float shootingCost;
    private float nextAttackTime;

    private void Awake()
    {
        playerMana = GetComponent<Mana>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _cooldownTimer > attackCooldown && _playerMovement.CanShoot() && playerMana.SpendMana(shootingCost))
        {
            _cooldownTimer = 0;
            _animator.SetTrigger("shoot");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1 / attackRate;
            _animator.SetTrigger("attack");
        }

        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.GetComponent<Health>() is null) continue;
            var enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.TakeDamage(meleeDamage);
        }
    }

    private void Shoot()
    {
        _cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (var i = 0; i < fireballs.Length; i++)
            if (!fireballs[i].activeInHierarchy)
                return i;
        return 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
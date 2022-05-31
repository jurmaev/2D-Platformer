using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private float dashDistance;
    [SerializeField] private float speed;
    [SerializeField] private float closeRange;
    [SerializeField] private float midRange;
    [SerializeField] private float closeRangeDamage;
    [SerializeField] private float midRangeDamage;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Transform firePoint;
    private Animator _animator;
    private Rigidbody2D _body;
    private Transform _player;
    private Health _playerHealth;
    private Health _bossHealth;

    public void Flip()
    {
        transform.localScale = transform.position.x >= _player.position.x
            ? new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 1f)
            : new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, 1f);
    }

    public void DamagePlayer(float damage)
    {
        _playerHealth.TakeDamage(damage);
    }

    public void Dash(Transform target)
    {
        _body.velocity = new Vector2(_body.velocity.x, 0f);
        _body.AddForce(new Vector2(dashDistance * Mathf.Sign(transform.localScale.x) * Time.deltaTime, 0f),
            ForceMode2D.Impulse);
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _player.position);
    }

    public void MoveToPlayer()
    {
        _body.velocity = new Vector2(speed * (transform.position.x >= _player.position.x ? -1 : 1), _body.velocity.y);
    }
    
    public void CloseRangeAttack()
    {
        Attack(closeRange, closeRangeDamage);
    }

    public void MidRangeAttack()
    {
        Attack(midRange, midRangeDamage);
    }

    private void Update()
    {
        if (_bossHealth.CurrentHealth < _bossHealth.GetMaxHealth() * 0.75 &&
            _bossHealth.CurrentHealth > _bossHealth.GetMaxHealth() * 0.25)
        {
            _animator.WriteDefaultValues();
            _animator.SetTrigger("upgrade");
        }
        else if (_bossHealth.CurrentHealth <= _bossHealth.GetMaxHealth() * 0.25 &&
                 _bossHealth.CurrentHealth > 0)
        {
            _animator.SetBool("isA", false);
            _animator.SetBool("isB", true);
        }
        else if(_bossHealth.CurrentHealth <= 0)
        {
            if (_animator.GetBool("isA"))
                _animator.SetTrigger("reset");
            _animator.WriteDefaultValues();
            _animator.SetTrigger("die");
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bossHealth = GetComponent<Health>();
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        _player = playerGO.GetComponent<Transform>();
        _playerHealth = playerGO.GetComponent<Health>();
        _body = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(playerGO.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }

    private void Shoot()
    {
        projectiles[FindProjectile()].transform.position = firePoint.position;
        projectiles[FindProjectile()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindProjectile()
    {
        for (var i = 0; i < projectiles.Length; i++)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }

    private void Attack(float attackRange, float damage)
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Player"));
        foreach (var col in hitColliders)
        {
            if (col.CompareTag("Player"))
                col.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.DrawWireSphere(transform.position, 11);
    }
}
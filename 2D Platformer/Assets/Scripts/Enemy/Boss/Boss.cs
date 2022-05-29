using System;
using System.Collections;
using System.Collections.Generic;
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
    // private float _cooldownTimer = Mathf.Infinity;
    [SerializeField] private Transform firePoint;
    private Animator _animator;
    private Rigidbody2D body;
    private Transform player;
    private Health playerHealth;
    private Health bossHealth;
    private bool isA;

    private bool isB;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        bossHealth = GetComponent<Health>();
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Transform>();
        playerHealth = playerGO.GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(playerGO.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
    
    private void Shoot()
    {
        projectiles[FindProjectile()].transform.position = firePoint.position;
        projectiles[FindProjectile()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindProjectile()
    {
        for (var i = 0; i < projectiles.Length; i++)
            if (!projectiles[i].activeInHierarchy)
                return i;
        return 0;
    }

    public void Flip()
    {
        transform.localScale = transform.position.x >= player.position.x
            ? new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 1f)
            : new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, 1f);
    }

    public void DamagePlayer(float damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public void Dash(Transform target)
    {
        // transform.position = Vector3.MoveTowards(transform.position, target.position, dashDistance * Time.deltaTime);
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(new Vector2(dashDistance * Mathf.Sign(transform.localScale.x) * Time.deltaTime, 0f),
            ForceMode2D.Impulse);
    }

    public float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }

    public void MoveToPlayer()
    {
        body.velocity = new Vector2(speed * (transform.position.x >= player.position.x ? -1 : 1), body.velocity.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (bossHealth.CurrentHealth >= bossHealth.GetMaxHealth() * 0.75)
        {
            // _animator.SetBool("isA", true);
            // isA = true;
        }
        else if (bossHealth.CurrentHealth < bossHealth.GetMaxHealth() * 0.75 &&
                 bossHealth.CurrentHealth > bossHealth.GetMaxHealth() * 0.25)
        {
            _animator.WriteDefaultValues();
            _animator.SetTrigger("upgrade");
        }
        else if (bossHealth.CurrentHealth <= bossHealth.GetMaxHealth() * 0.25 &&
                 bossHealth.CurrentHealth > 0)
        {
            _animator.SetBool("isA", false);
            _animator.SetBool("isB", true);
            // isA = false;
            // isB = true;
        }
        else
        {
            if(_animator.GetBool("isA"))
                _animator.SetTrigger("reset");
            _animator.WriteDefaultValues();
            _animator.SetTrigger("die");
        }
            
    }
    
    public void CloseRangeAttack()
    {
        Attack(closeRange, closeRangeDamage);
    }

    public void MidRangeAttack()
    {
        Attack(midRange, midRangeDamage);
    }

    private void Attack(float attackRange, float damage)
    {
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Player"));
        foreach (var col in hitColliders)
        {
            if(col.CompareTag("Player"))
                col.GetComponent<Health>().TakeDamage(damage);
            // if (col.GetType() != typeof(BoxCollider2D)) continue;
            // if (col.GetComponent<Health>() is null) continue;
            // var enemyHealth = col.GetComponent<Health>();
            // enemyHealth.TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.DrawWireSphere(transform.position, 11);
    }
}
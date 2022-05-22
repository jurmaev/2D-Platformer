using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float range;
    [SerializeField] protected int damage;
    [Header("Collider Parameters")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] protected LayerMask playerLayer;
    
    protected float _cooldownTimer = Mathf.Infinity;
    protected Animator _animator;
    protected EnemyPatrol _enemyPatrol;
    
    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
        Physics2D.IgnoreCollision(boxCollider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator _animator;
    private Transform _target;
    private Health _health;
    [SerializeField] private float zoneRadius;
    private Rigidbody2D body;
    private SpriteRenderer sprite;


    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }   

    private void Update()
    {
        if (!CheckZone()) return;
        sprite.flipX = transform.position.x >= _target.position.x;
        if (!_health._dead)
            body.velocity = new Vector2( speed * (sprite.flipX ? -1 : 1), body.velocity.y);
        _animator.SetBool("moving", true);
    }

    private bool CheckZone()
    {
        var objectsInZone = Physics2D.OverlapCircleAll(transform.position, zoneRadius);
        foreach(var obj in objectsInZone)
        {
            if (obj.CompareTag("Player"))
                return true;
        }
        _animator.SetBool("moving", false);
        return false;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }
    
    private void OnDisable()
    {
        _animator.SetBool("moving", false);
    }
}

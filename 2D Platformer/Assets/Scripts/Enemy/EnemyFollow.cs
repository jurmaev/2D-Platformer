using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoneRadius;
    [SerializeField] private float jumpRadius;
    private Animator _animator;
    private Health _health;
    private Transform _target;
    private Rigidbody2D body;
    private bool isGrounded;


    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("grounded", isGrounded);
        if (!CheckForPlayer()) return;
        if(CheckForBlock())
            Jump();
        transform.localScale = transform.position.x >= _target.position.x
            ? new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 1f)
            : new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, 1f);
        if (!_health._dead)
            body.velocity = new Vector2(speed * (transform.position.x >= _target.position.x  ? -1 : 1), body.velocity.y);
        _animator.SetBool("moving", true);
    }
    
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed * 1.5f);
        _animator.SetTrigger("jump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private bool CheckForPlayer()
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

    private bool CheckForBlock()
    {
        var objectsInZone = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + Mathf.Sign(transform.localScale.x) * 0.9f, transform.position.y -0.3f), jumpRadius);
        foreach(var obj in objectsInZone)
        {
            if (obj.CompareTag("Ground"))
                return true;
        }
        _animator.SetBool("moving", false);
        return false; 
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x + Mathf.Sign(transform.localScale.x) * 0.9f, transform.position.y -0.3f, transform.position.z), jumpRadius);
    }
    
    private void OnDisable()
    {
        _animator.SetBool("moving", false);
    }
}

using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zoneRadius;
    [SerializeField] private float jumpRadius;
    [Header("Jump Zone Offset")] [SerializeField]
    private float x;
    [SerializeField] private float y;
    [SerializeField] private float distanceToPlayer;
    private Animator _animator;
    private Health _health;
    private Transform _target;
    private Rigidbody2D _body;
    private bool _isGrounded;
    private EnemyPatrol _enemyPatrol;


    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _body = GetComponent<Rigidbody2D>();
        _enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("grounded", _isGrounded);

        if (_enemyPatrol != null)
            _enemyPatrol.enabled = !CheckForPlayer();
        if (GetDistanceToPlayer() <= distanceToPlayer)
            return;
        if (!CheckForPlayer()) return;
        if (CheckForBlock() && _isGrounded)
            Jump();
        transform.localScale = transform.position.x >= _target.position.x
            ? new Vector3(Math.Abs(transform.localScale.x) * -1, transform.localScale.y, 1f)
            : new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y, 1f);
        if (!_health.Dead)
            _body.velocity = new Vector2(speed * (transform.position.x >= _target.position.x ? -1 : 1), _body.velocity.y);
        _animator.SetBool("moving", true);
    }

    private float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, _target.transform.position);
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, speed * 3f);
        _animator.SetTrigger("jump");
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            _isGrounded = true;
    }

    private bool CheckForPlayer()
    {
        var objectsInZone = Physics2D.OverlapCircleAll(transform.position, zoneRadius);
        foreach (var obj in objectsInZone)
        {
            if (obj.CompareTag("Player"))
                return true;
        }

        _animator.SetBool("moving", false);
        return false;
    }

    private bool CheckForBlock()
    {
        var objectsInZone = Physics2D.OverlapCircleAll(
            new Vector2(transform.position.x + Mathf.Sign(transform.localScale.x) * x, transform.position.y - y),
            jumpRadius);
        foreach (var obj in objectsInZone)
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
        Gizmos.DrawWireSphere(
            new Vector3(transform.position.x + Mathf.Sign(transform.localScale.x) * x, transform.position.y - y,
                transform.position.z), jumpRadius);
    }

    private void OnDisable()
    {
        _animator.SetBool("moving", false);
    }
}
using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float resetTime;
    protected bool Hit;
    protected BoxCollider2D Collider;
    protected Animator Animator;
    private float _direction;
    private float _lifetime;

    public void SetDirection(float direction)
    {
        _lifetime = 0;
        _direction = direction;
        gameObject.SetActive(true);
        Hit = false;
        Collider.enabled = true;

        var localScaleX = transform.localScale.x;
        if (Math.Sign(localScaleX) != direction)
            localScaleX *= -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Awake()
    {
        Collider = GetComponent<BoxCollider2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Hit)
            return;
        var movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);
        _lifetime += Time.deltaTime;
        if (_lifetime > resetTime) gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        Collider.enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        Hit = true;
        Animator.SetTrigger("explode");
        if (col.CompareTag("Enemy") && col is BoxCollider2D)
            col.GetComponent<Health>().TakeDamage(damage);
    }
    
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
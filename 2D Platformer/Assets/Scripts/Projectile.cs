using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected float resetTime;
    protected bool _hit;
    protected float _direction;
    protected float _lifetime;
    
    protected BoxCollider2D _collider;
    protected Animator _animator;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_hit)
            return;
        var movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);
        _lifetime += Time.deltaTime;
        if(_lifetime > resetTime) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _collider.enabled = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        _hit = true;
        _animator.SetTrigger("explode");
        if (col.CompareTag("Enemy"))
            col.GetComponent<Health>().TakeDamage(damage);
    }

    public void SetDirection(float direction)
    {
        _lifetime = 0; 
        _direction = direction;
        gameObject.SetActive(true);
        _hit = false;
        _collider.enabled = true;

        var localScaleX = transform.localScale.x;
        if (Math.Sign(localScaleX) != direction)
            localScaleX *= -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

using System;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float damage;
    
    private float _lifetime;
    private Animator _anim;
    private BoxCollider2D _collider;
    private float _direction;
    private bool _hit;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (_hit) return;
        var movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);

        _lifetime += Time.deltaTime;
        if (_lifetime > resetTime)
            gameObject.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        _hit = true;
        _collider.enabled = false;
      
        _anim.SetTrigger("explode");
        if (collision.CompareTag("Player"))
            collision.GetComponent<Health>().TakeDamage(damage);
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

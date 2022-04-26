using System;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    private Animator _animator;
    private Transform _target;
    private Health _health;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        if (transform.position.x >= _target.position.x)
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * -1, Math.Abs(transform.localScale.y), 1f);
        else
            transform.localScale = new Vector3(Math.Abs(transform.localScale.x), Math.Abs(transform.localScale.y), 1f);
        if(!_health.IsDead())
        transform.position = new Vector3(transform.position.x + Time.deltaTime  * speed * (_target.transform.position - transform.position).normalized.x, transform.position.y,
            transform.position.z);
        _animator.SetBool("moving", true);
    }
    private void OnDisable()
    {
        _animator.SetBool("moving", false);
    }
}

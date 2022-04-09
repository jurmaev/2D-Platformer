using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _fireballs;
    private float _cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _cooldownTimer > attackCooldown && _playerMovement.CanShoot())
            Attack();
        _cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _animator.SetTrigger("shoot");
        _cooldownTimer = 0;

        _fireballs[0].transform.position = _firePoint.position;
        _fireballs[0].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (var i = 0; i < _fireballs.Length; i++)
            if (!_fireballs[i].activeInHierarchy)
                return i;
        return 0;
    }
}

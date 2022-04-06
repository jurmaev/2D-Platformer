using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _body;
    private Animator _animator;
    private bool _isGrounded;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y);
        
        if (horizontalInput > .01f)
            transform.localScale = new Vector3(5, 5, 1);
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(-5, 5, 1);

        if (Input.GetKey(KeyCode.Space) && _isGrounded) Jump();
        
        _animator.SetBool("run", horizontalInput != 0);
        _animator.SetBool("grounded", _isGrounded);
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, speed * 2);
        _animator.SetTrigger("jump");
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            Debug.Log("ground");
        }
    }
}

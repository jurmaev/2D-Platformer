using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float deathLevel;
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
            transform.localScale = Vector3.one;
        else if (horizontalInput < -.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && _isGrounded) Jump();
        
        if(transform.position.y < deathLevel)
            GetComponent<GameController>().GameOver();
        _animator.SetBool("run", horizontalInput != 0);
        _animator.SetBool("grounded", _isGrounded);
    }

    private void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, speed * 1.5f);
        _animator.SetTrigger("jump");
        _isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
            _isGrounded = true;
    }

    public bool CanShoot()
    {
        return Input.GetAxis("Horizontal") == 0 && _isGrounded;
    }
}
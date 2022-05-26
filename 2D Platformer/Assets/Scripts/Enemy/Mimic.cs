using UnityEngine;

public class Mimic : MeleeEnemy
{
    [SerializeField] private float zoneRadius;
    [SerializeField] private float lickCooldown;
    [SerializeField] private float jumpForce;
    // [SerializeField] private float x;
    // [SerializeField] private float y;
    // [SerializeField] private float jumpRadius;
    // [SerializeField] private float jumpCooldown;
    // private float jumpTimer;
    private float _lickTimer = Mathf.Infinity;
    private Rigidbody2D body;
    private bool isGrounded;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _lickTimer += Time.fixedDeltaTime;
        // jumpTimer += Time.fixedDeltaTime;
        if (CheckForPlayer() && _lickTimer >= lickCooldown)
        {
            Lick();
            _lickTimer = 0;
        }

        if (body.velocity.x != 0 && isGrounded )
        {
            // _animator.SetTrigger("jump");
            Jump();
            // jumpTimer = 0;
        }
    }

    private void Lick()
    {
        _animator.SetTrigger("lick");
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        // _animator.SetTrigger("jump");
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
        foreach (var obj in objectsInZone)
        {
            if (obj.CompareTag("Player"))
                return true;
        }

        _animator.SetBool("moving", false);
        return false;
    }

    // private bool CheckForBlock()
    // {
    //     var objectsInZone = Physics2D.OverlapCircleAll(
    //         new Vector2(transform.position.x + Mathf.Sign(transform.localScale.x) * x, transform.position.y - y),
    //         jumpRadius);
    //     foreach (var obj in objectsInZone)
    //     {
    //         if (obj.CompareTag("Ground"))
    //             return true;
    //     }
    //
    //     _animator.SetBool("moving", false);
    //     return false;
    // }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }
}
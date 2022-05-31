using UnityEngine;

public class Mimic : MeleeEnemy
{
    [SerializeField] private float zoneRadius;
    [SerializeField] private float lickCooldown;
    [SerializeField] private float jumpForce;
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
        if (CheckForPlayer() && _lickTimer >= lickCooldown)
        {
            Lick();
            _lickTimer = 0;
        }

        if (Mathf.Abs(body.velocity.x) > 1e-5 && isGrounded )
        {
            Jump();
        }
    }

    private void Lick()
    {
        Animator.SetTrigger("lick");
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
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

        Animator.SetBool("moving", false);
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }
}
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float _lifetime;
    private Animator _anim;
    private BoxCollider2D _coll;

    private bool _hit;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        _hit = false;
        _lifetime = 0;
        gameObject.SetActive(true);
        _coll.enabled = true;
    }
    private void Update()
    {
        if (_hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        _lifetime += Time.deltaTime;
        if (_lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        base.OnTriggerEnter2D(collision); 
        _coll.enabled = false;
        if(_anim != null)
            _anim.SetTrigger("explode");
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
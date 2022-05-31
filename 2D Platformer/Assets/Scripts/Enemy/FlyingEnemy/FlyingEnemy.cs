using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform startingPosition;
    [SerializeField] private float damage;
    public bool chase;
    private Animator _animator;
    private GameObject _player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (_player == null)
            return;
        if (chase)
        {
            ChasePlayer();
            _animator.SetBool("moving", true);
        }
        else if (transform.position == startingPosition.position)
            _animator.SetBool("moving", false);
        else
            ReturnStartingPosition();

        Flip();
    }

    private void ChasePlayer()
    {
        transform.position =
            Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartingPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPosition.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x < _player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            col.GetComponent<Health>().TakeDamage(damage);
    }
}
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")] [SerializeField]
    private Transform leftEdge;

    [SerializeField] private Transform rightEdge;

    [Header("Enemy")] [SerializeField] private Transform enemy;

    [Header("Movement parameters")] [SerializeField]
    private float speed;

    private Vector3 _initScale;
    private bool _movingLeft;

    [Header("Idle Behaviour")] [SerializeField]
    private float idleDuration;
    private float _idleTimer;

    [Header("Enemy Animator")] [SerializeField]
    private Animator animator;

    private void Awake()
    {
        _initScale = enemy.localScale;
    }

    private void FixedUpdate()
    {
        if (_movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);
        _idleTimer += Time.deltaTime;
        if(_idleTimer > idleDuration)
            _movingLeft = !_movingLeft;
    }

    private void MoveInDirection(int direction)
    {
        _idleTimer = 0;
        animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(_initScale.x) * direction, _initScale.y, _initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y,
            enemy.position.z);
    }
}
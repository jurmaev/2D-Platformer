using UnityEngine;

public class Run : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    private Boss _boss;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = _boss.GetDistanceToPlayer();
        if (distanceToPlayer > closeRange)
            _boss.MoveToPlayer();
        else
            animator.SetBool("moving", false);
    }
}
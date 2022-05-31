using UnityEngine;

public class idleB : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    private Boss _boss;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        animator.SetBool("isA", false);
        animator.SetBool("isB", true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = _boss.GetDistanceToPlayer();
        _boss.Flip();
        if (distanceToPlayer > closeRange)
            animator.SetBool("moving", true);
        else
            animator.SetTrigger("attackA");
    }
}
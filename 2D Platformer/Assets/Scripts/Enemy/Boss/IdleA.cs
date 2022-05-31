using UnityEngine;

public class IdleA : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    [SerializeField] private float midRange;
    private Boss _boss;
    private Health _bossHealth;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _bossHealth = _boss.GetComponent<Health>();
        animator.SetBool("isA", true);
        animator.SetBool("isB", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = _boss.GetDistanceToPlayer();
        _boss.Flip();
        if (_bossHealth.CurrentHealth < _bossHealth.GetMaxHealth() * 0.75 &&
            _bossHealth.CurrentHealth >= _bossHealth.GetMaxHealth() * 0.25)
        {
            animator.SetTrigger("dieA");
            animator.SetBool("deadA", true);
        }

        if (distanceToPlayer <= closeRange)
            animator.SetTrigger("attackB");
        else if (distanceToPlayer > closeRange && distanceToPlayer <= midRange)
            animator.SetTrigger("attackC");
        else
            animator.SetTrigger("reset");
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("dieA");
    }
}
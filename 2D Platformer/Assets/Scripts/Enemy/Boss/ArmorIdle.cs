using UnityEngine;

public class ArmorIdle : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    [SerializeField] private float midRange;
    [SerializeField] private float longRange;
    private Boss _boss;
    private Health _bossHealth;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss = animator.GetComponent<Boss>();
        _bossHealth = _boss.GetComponent<Health>();
        animator.ResetTrigger("upgrade");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = _boss.GetDistanceToPlayer();
        _boss.Flip();
        if (_bossHealth.CurrentHealth <= 0.25)
            animator.SetTrigger("breakArmor");
        if (distanceToPlayer <= closeRange)
            animator.SetTrigger("armorAttackA");
        else if (distanceToPlayer > closeRange && distanceToPlayer <= midRange)
            animator.SetTrigger("armorAttackB");
        else if (distanceToPlayer > midRange && distanceToPlayer <= longRange)
            animator.SetTrigger("armorAttackC");
        else
            animator.SetBool("moving", true);
    }
}
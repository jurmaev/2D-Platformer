using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorIdle : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    [SerializeField] private float midRange;
    [SerializeField] private float longRange;
    // [SerializeField] private float longRange;
    private Transform bossPos;
    private Boss boss;
    private Transform player;
    private Health bossHealth;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPos = animator.GetComponent<Transform>();
        boss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        bossHealth = boss.GetComponent<Health>();
        animator.ResetTrigger("upgrade");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = boss.GetDistanceToPlayer();
        boss.Flip();
        // Debug.Log(distanceToPlayer);
        if(bossHealth.CurrentHealth <= 0.25)
            animator.SetTrigger("breakArmor");
        if(distanceToPlayer <= closeRange)
            animator.SetTrigger("armorAttackA");
        else if(distanceToPlayer > closeRange && distanceToPlayer <= midRange)
            animator.SetTrigger("armorAttackB");
        else if(distanceToPlayer > midRange && distanceToPlayer <= longRange)
            animator.SetTrigger("armorAttackC");
        else
            animator.SetBool("moving", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

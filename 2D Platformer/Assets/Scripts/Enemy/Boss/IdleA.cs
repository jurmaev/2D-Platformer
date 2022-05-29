using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleA : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    [SerializeField] private float midRange;
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
        animator.SetBool("isA", true);
        animator.SetBool("isB", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = boss.GetDistanceToPlayer();
        boss.Flip();
        // Debug.Log(distanceToPlayer);
        if (bossHealth.CurrentHealth < bossHealth.GetMaxHealth() * 0.75 &&
            bossHealth.CurrentHealth >= bossHealth.GetMaxHealth() * 0.25)
        {
            animator.SetTrigger("dieA");
            animator.SetBool("deadA", true);
        }

        if(distanceToPlayer <= closeRange)
            animator.SetTrigger("attackB");
        else if (distanceToPlayer > closeRange && distanceToPlayer <= midRange)
        {
            animator.SetTrigger("attackC");
            // boss.Dash(player);
        }
        else
            animator.SetTrigger("reset");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("dieA");   
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

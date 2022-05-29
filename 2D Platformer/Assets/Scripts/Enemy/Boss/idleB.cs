using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleB : StateMachineBehaviour
{
    [SerializeField] private float closeRange;
    // [SerializeField] private float attackCooldown;
    // private float _cooldownTimer = Mathf.Infinity;
    private Transform bossPos;
    private Boss boss;

    private Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPos = animator.GetComponent<Transform>();
        boss = animator.GetComponent<Boss>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator.SetBool("isA", false);
        animator.SetBool("isB", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distanceToPlayer = boss.GetDistanceToPlayer() ;
        // _cooldownTimer += Time.deltaTime;
        boss.Flip();
        // Debug.Log(distanceToPlayer);
        if (distanceToPlayer > closeRange)
        {
            // boss.MoveToPlayer();
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetTrigger("attackA");
        }
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

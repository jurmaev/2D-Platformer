using UnityEngine;

public class AttackC : StateMachineBehaviour
{
    private Transform _player;
    private Boss _boss;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _boss = animator.GetComponent<Boss>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _boss.Dash(_player);
    }
}

using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 3)
            animator.SetBool("isPatrolling", true);
    }
}
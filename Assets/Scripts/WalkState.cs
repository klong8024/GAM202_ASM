using UnityEngine;

public class IdleWalkState : StateMachineBehaviour
{
    float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
        if (time > 7)
            animator.SetBool("isPatrolling", false);
    }
}
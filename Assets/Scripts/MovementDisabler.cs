using UnityEngine;

public class MovementDisabler : StateMachineBehaviour
{
    private WeaponManager weaponManager;

    public enum Option {Enable, Disable}
    public Option enableMovement;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weaponManager = animator.GetComponent<WeaponManager>();

        if (enableMovement == Option.Enable)
            weaponManager.EnableMovement(true);
        else weaponManager.EnableMovement(false);
    }
}

using Invector.vCharacterController;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weapon;
    public vThirdPersonController tcp; //Bổ sung
    public void EnableWeaponCollider (int isEnable)
    {
        if (weapon != null)
        {
            Collider col = weapon.GetComponent<Collider>();
            if (col != null)
            {
                if (isEnable == 1)
                    col.enabled = true;
                else
                    col.enabled = false;
            }
        }
    }
    public void EnableMovement (bool enable) //Bổ sung hàm 
    {
        if (tcp == null) return;

        if (enable == false)
        {
            tcp.lockMovement = true;
            tcp.lockRotation = true;
        }
        else
        {
            tcp.lockMovement = false;
            tcp.lockRotation = false;
        }
    }
}

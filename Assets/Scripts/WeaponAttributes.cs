using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    public AttributesManager atm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = atm.DealDamage(other.gameObject);

            if (damage > 0)
            {
                DamagePopUpGenerator.current.CreatePopUp(DamagePopUpGenerator.current.headPoint.position, damage);
            }
        }
            
    }
}

using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    public enum OwnerType { Player, Enemy }
    public OwnerType owner;
    public AttributesManager atm;

    private void OnTriggerEnter(Collider other)
    {
        if (owner == OwnerType.Player && other.CompareTag("Enemy"))
        {
            int damage = atm.DealDamage(other.gameObject);
            if (damage > 0)
            {
                DamagePopUpGenerator.current.CreatePopUp(
                    DamagePopUpGenerator.current.headPoint.position, damage);
            }
        }

        if (owner == OwnerType.Enemy && other.CompareTag("Player"))
        {
            Debug.Log("Enemy chem Player");
            atm.DealDamage(other.gameObject);
        }
    }
}

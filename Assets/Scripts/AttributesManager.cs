using UnityEngine;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int damage;
    public int armor; //Giáp

    public float critDamage = 1.5f; //Sát thương chí mạng 
    public float critChance = 0.5f; //Tỉ lệ chí mạng

    public int TakeDamage(int amount) // void -> int
    {
        //Giảm sát thương theo % giáp
        int finalDamage = amount - (amount * armor / 100);

        health -= finalDamage;

        Debug.Log(gameObject.name + " take damage: " + finalDamage);

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " is dead!");

            Enemy enemy = GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }
        }

        return finalDamage; //Trả về giá trị số nguyên
    }

    public int DealDamage(GameObject target) // void -> int
    {
        AttributesManager atm = target.GetComponent<AttributesManager>();

        if (atm == null) return 0;

        float totalDamage = damage;

        if (Random.Range(0f, 1f) < critChance)
        {
            totalDamage *= critDamage;
            Debug.Log("Critical Hit!");
        }

        int finalDamage = atm.TakeDamage((int)totalDamage);

        return finalDamage; //Trả về giá trị số nguyên
    }
}


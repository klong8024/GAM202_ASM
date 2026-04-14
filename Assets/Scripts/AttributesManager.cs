using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AttributesManager : MonoBehaviour
{
    public int health;
    public int damage;
    public int armor;
    public bool isDead = false;
    public GameObject gemPrefab;
    public float critDamage = 1.5f; //Sát thương chí mạng 
    public float critChance = 0.5f; //Tỉ lệ chí mạng

    //Thanh máu hướng về Player
    private Slider healthSlider;
    private Transform healthCanvas;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (!CompareTag("Enemy")) return;
        healthCanvas = transform.Find("Canvas");

        Transform bar = healthCanvas.Find("HealthBar");
        if (bar == null)
        {
            Debug.LogError("Không tìm thấy HealthBar trong Canvas: " + gameObject.name);
            return;
        }

        healthSlider = bar.GetComponent<Slider>();
        if (healthSlider == null)
        {
            Debug.LogError("HealthBar không có Slider component");
            return;
        }

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void Update()
    {
        if (healthCanvas != null && mainCamera != null)
        {
            healthCanvas.LookAt(
                healthCanvas.position + mainCamera.transform.forward, mainCamera.transform.up
            );
        }
    }

    public int TakeDamage(int amount) // void -> int
    {
        //Giảm sát thương theo % giáp
        int finalDamage = amount - (amount * armor / 100);

        health -= finalDamage;

        Debug.Log(gameObject.name + " take damage: " + finalDamage);

        if (gameObject.CompareTag("Enemy"))
        {
            Slider slider = gameObject.transform.GetChild(1).transform.GetChild(0).transform.GetComponent<Slider>(); 
            slider.value = health;
        }

        if (health <= 0)
        {
            if (CompareTag("Enemy"))
                EnemyDie();
            else if (CompareTag("Player"))
                PlayerDie();
        }

        return finalDamage; //Trả về giá trị số nguyên
    }

        void EnemyDie()
        {
            if (isDead) return;
            isDead = true;
            Debug.Log(gameObject.name + " is dead!");

            EnemyController enemyController = GetComponent<EnemyController>();

            if (enemyController != null)
                enemyController.enabled = false;

            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (agent != null)
            {
                agent.isStopped = true;
                agent.enabled = false;
            }

            CharacterController cc = GetComponent<CharacterController>();

            if (cc != null)
                cc.enabled = false;

            Collider col = GetComponent<Collider>();

            if (col != null)
                col.enabled = false;

            Animator animator = GetComponentInChildren<Animator>();

            if (animator != null)
            {
                animator.applyRootMotion = false;
                animator.SetBool("isDead", true);
            }

            if (gemPrefab != null)
            {
                GameObject gem = Instantiate(
                    gemPrefab,
                    transform.position + Vector3.up * 0.5f,
                    Quaternion.identity
                );
                gem.SetActive(true);
            }

            Destroy(gameObject, 2f);
        }
    
        void PlayerDie()
        {
            Debug.Log("Player Dead");

            // Tắt script điều khiển
            var scripts = GetComponents<MonoBehaviour>();
            foreach (var s in scripts)
            {
                if (s != this) s.enabled = false;
            }

            // Play animation
            Animator anim = GetComponent<Animator>();
            if (anim != null)
                anim.SetTrigger("Dead");

            // Freeze Rigidbody để không rơi
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            // Tắt collider sau 1.5s
            StartCoroutine(DisableColliderAfterDelay(1.5f));

            // Destroy sau 3s
            Destroy(gameObject, 3f);
        }
        IEnumerator DisableColliderAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            var col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
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


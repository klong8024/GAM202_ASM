using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        animator.SetTrigger("Die");

        Destroy(gameObject, 3f);
    }
}

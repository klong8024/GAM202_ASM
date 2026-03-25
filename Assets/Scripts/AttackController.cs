using UnityEngine;

public class AttackController : MonoBehaviour
{
    Animator anim;
    private string horizontalInput = "Horizontal";
    private string verticalInput = "Vertical";

    public ParticleSystem slashVfx; //Bổ sung

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool isMoving =
                Mathf.Abs(Input.GetAxis(horizontalInput)) > 0.05f || 
                Mathf.Abs(Input.GetAxis(verticalInput)) > 0.05f;

            if (isMoving)
                anim.SetTrigger("SlashMask");
            else
                anim.SetTrigger("Slash");
        }
    }

    private void PlaySlashVfx() //Bổ sung
    {
        if (slashVfx != null)
            slashVfx.Play();
    }
}
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float gravity = -20f;
    CharacterController controller;
    float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        yVelocity = -1f;
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            if (yVelocity < 0)
                yVelocity = -2f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        controller.Move(Vector3.up * yVelocity * Time.deltaTime);
    }
}

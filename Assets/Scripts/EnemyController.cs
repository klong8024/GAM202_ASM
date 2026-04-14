using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    Transform[] waypoints;
    int currentIndex;
    public float walkSpeed = 2f;
    public float gravity = -20f;
    CharacterController controller;
    float yVelocity;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        GameObject waypointsObject = GameObject.FindGameObjectWithTag("WayPoints");
        waypoints = new Transform [waypointsObject.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)        
        {
            waypoints[i] = waypointsObject.transform.GetChild(i);
        }
        currentIndex = Random.Range(0, waypoints.Length);
        agent.speed = walkSpeed;
        agent.SetDestination(waypoints[currentIndex].position);

        controller = GetComponent<CharacterController>();
        yVelocity = -1f;
    }

    void Update()
    {
        float speed = agent.velocity.magnitude;

        animator.SetBool("isPatrolling", speed > 0.1f);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[currentIndex].position);
        }
    }
}

using UnityEngine;

public class MoveRBK : MonoBehaviour
{
    public enum CharState
    {
        ClimbingDown,
        Walking,
        ClimbingUp,
        ClimbingOver
    }

    public Transform target;
    public float walkingSpeed = 3f;
    public float climbingSpeed = 2f;
    public float climbingOverSpeed = 5f;
    public float gravity = 9.8f;
    public LayerMask obstacleLayer;

    private Rigidbody rb;
    private CharState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = CharState.ClimbingDown;
    }

    private void Update()
    {
        switch (currentState)
        {
            case CharState.ClimbingDown:
                MoveClimbingDown();
                break;
            case CharState.Walking:
                MoveWalking();
                break;
            case CharState.ClimbingUp:
                MoveClimbingUp();
                break;
            case CharState.ClimbingOver:
                MoveClimbingOver();
                break;
        }
    }

    private void MoveClimbingDown()
    {
        // Move character towards the ground
        Vector3 downDirection = -transform.up;
        rb.MovePosition(rb.position + downDirection * gravity * Time.deltaTime);

        // Check if near the ground
        if (Physics.Raycast(transform.position, downDirection, out RaycastHit hit, 1f))
        {
            currentState = CharState.Walking;
        }
    }

    private void MoveWalking()
    {
        // Move character towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * walkingSpeed * Time.deltaTime);

        // Check for obstacle collision
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f, obstacleLayer))
        {
            currentState = CharState.ClimbingUp;
        }
    }

    private void MoveClimbingUp()
    {
        // Move character upwards
        Vector3 upDirection = transform.up;
        rb.MovePosition(rb.position + upDirection * climbingSpeed * Time.deltaTime);

        // Check for obstacle exit
        if (!Physics.Raycast(transform.position, upDirection, 1f, obstacleLayer))
        {
            currentState = CharState.ClimbingOver;
            Invoke(nameof(ChangeToWalkingState), 1f);
        }
    }

    private void MoveClimbingOver()
    {
        // Move character towards the target
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0f; // Ignore vertical movement
        rb.MovePosition(rb.position + direction * climbingOverSpeed * Time.deltaTime);
    }

    private void ChangeToWalkingState()
    {
        currentState = CharState.Walking;
    }
}

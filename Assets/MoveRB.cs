using UnityEngine;

public class MoveRB : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5f;
    public float stoppingDistance = 0.1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate the direction to the target
        Vector3 direction = target.position - transform.position;
        direction.y = 0f; // Ignore height difference

        // Rotate the character toward the target
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        // Move the character toward the target
        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            Vector3 movement = direction.normalized * moveSpeed * Time.deltaTime;
            rb.AddForce(movement, ForceMode.VelocityChange);
        }

        // Check if the character has reached the target
        if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}

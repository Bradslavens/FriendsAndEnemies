using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the other object when trigger collision occurs
        Destroy(other.gameObject);
    }
}

using UnityEngine;

public class DeliveryGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("You Win!");
        }
    }
}

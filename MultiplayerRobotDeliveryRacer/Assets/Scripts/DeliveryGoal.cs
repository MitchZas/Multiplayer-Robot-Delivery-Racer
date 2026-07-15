using UnityEngine;

public class DeliveryGoal : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the "Player" tag
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth == null) return;

            if (playerHealth.currentHealth > 60 && playerHealth.currentHealth <= 100)
            {
                Debug.Log("You made $8!");
            }
            else if (playerHealth.currentHealth > 40 && playerHealth.currentHealth <= 60)
            {
                Debug.Log("You made $4!");
            }
            else
            {
                Debug.Log("You made $2!");
            }
        }
    }
}

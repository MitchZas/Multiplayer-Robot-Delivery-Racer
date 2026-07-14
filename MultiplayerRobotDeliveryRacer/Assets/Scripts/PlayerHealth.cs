using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (healthBar == null)
        {
            healthBar = FindFirstObjectByType<HealthBar>();
        }
        Debug.Log("healthBar is null: " + (healthBar == null));

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cube")) TakeDamage(10); 

        if (other.gameObject.CompareTag("NPC")) TakeDamage(20);

        if (other.gameObject.CompareTag("Building")) TakeDamage(30);
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        Debug.Log(currentHealth);
    }
}

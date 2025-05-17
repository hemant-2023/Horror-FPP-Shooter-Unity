using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI Settings")]
    public Image healthBarForeground; // Assign this to the Green fill bar
    public Canvas healthBarCanvas;    // The canvas that holds the health bar

    void Start()
    {
        currentHealth = maxHealth;

        // Hide health bar at full health if you want
        if (healthBarCanvas != null)
            healthBarCanvas.enabled = false;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBarForeground != null)
        {
            healthBarForeground.fillAmount = currentHealth / maxHealth;
        }

        if (healthBarCanvas != null)
        {
            healthBarCanvas.enabled = true; // Show health bar after taking damage
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Destroy enemy (you can replace with death animation later)
    }
}

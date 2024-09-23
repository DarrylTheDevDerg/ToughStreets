using UnityEngine;
using TMPro;  // Import TextMeshPro namespace for text handling

public class HealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText;  // Reference to the TextMeshProUGUI component
    public Color startColor = Color.white;  // Color at full health
    public Color endColor = Color.red;  // Color nearing zero health

    public float currentHealth, maxHealth;  // Current health value

    private PlayerStats pS;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        maxHealth = pS.healthPoints;
    }

    void Update()
    {
        UpdateHealthText();
        currentHealth = pS.healthPoints;
    }

    void UpdateHealthText()
    {
        float healthPercentage = currentHealth / maxHealth;
        Color healthColor = Color.Lerp(endColor, startColor, healthPercentage);

        // Flash red if health is below 20% as an example
        if (healthPercentage < 0.2f)
        {
            float flash = Mathf.Abs(Mathf.Sin(Time.time * 5));  // Create a flashing effect
            healthColor = Color.Lerp(healthColor, Color.red, flash);
        }

        healthText.color = healthColor;
    }
}

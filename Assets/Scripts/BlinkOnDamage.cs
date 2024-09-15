using UnityEngine;
using System.Collections;

public class BlinkOnDamage : MonoBehaviour
{
    public Color blinkColor = Color.red;    // Color to blink (e.g., red for damage)
    public float blinkDuration = 0.1f;      // Duration of each blink
    public int blinkCount = 1;              // How many times the object should blink
    private Renderer objectRenderer;        // Reference to the object's renderer
    private Color originalColor;            // Original color of the object

    void Start()
    {
        // Get the Renderer component
        objectRenderer = GetComponent<Renderer>();

        // Store the original color of the object
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    // Call this method to make the object blink (e.g., on damage)
    public void TriggerBlink()
    {
        if (objectRenderer != null)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // Change to blink color
            objectRenderer.material.color = blinkColor;

            // Wait for blink duration
            yield return new WaitForSeconds(blinkDuration);

            // Revert to the original color
            objectRenderer.material.color = originalColor;

            // Wait again before next blink
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}

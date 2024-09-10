using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private PlayerStats pS;
    public int hpAmount;
    public string playerTag;

    public float itemLifetime;
    public float fadeDuration;

    private float currentTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            HealPlayer();
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();    
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > itemLifetime)
        {
            StartCoroutine(FadeOut());
        }
    }

    void HealPlayer()
    {
        pS.healthPoints += hpAmount;
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // Gradually reduce alpha
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            spriteRenderer.color = newColor;

            yield return null;  // Wait until the next frame
        }

        // Ensure alpha is set to 0 at the end
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Destroy the object after fade-out
        Destroy(gameObject);
    }
}

[CustomEditor(typeof(HealingItem))]
[CanEditMultipleObjects]
public class HIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        HealingItem item = (HealingItem)target;

        // Check if the Collider is missing
        int amount = item.hpAmount;

        string player = item.playerTag;

        float lifetime = item.itemLifetime;

        if (player == null)
        {
            EditorGUILayout.HelpBox("You need to input the Player tag for this to work.", MessageType.Error);
        }

        if (amount == 0)
        {
            EditorGUILayout.HelpBox("Amount must be bigger than 0 to work properly.", MessageType.Error);
        }

        if (lifetime == 0)
        {
            EditorGUILayout.HelpBox("Lifetime value must be bigger than 0 to work properly, otherwise, it'll get destroyed immediately.", MessageType.Error);
        }
    }
}

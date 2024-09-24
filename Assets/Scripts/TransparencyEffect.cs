using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyEffect : MonoBehaviour
{
    public Renderer characterRenderer;
    public float invulnerabilityDuration = 3f;
    public float transparencyLevel = 0.3f;  // 0 fully transparent, 1 fully opaque
    private bool isInvulnerable = false;
    private Material characterMaterial;
    private Color originalColor;
    private PlayerAttack pA;

    void Start()
    {
        pA = GetComponent<PlayerAttack>();
        invulnerabilityDuration = pA.iFrames;
        characterMaterial = characterRenderer.material;
        originalColor = characterMaterial.color;
    }

    public void TriggerInvulnerability()
    {
        if (!isInvulnerable)
        {
            StartCoroutine(TransparencyEffectCoroutine());
        }
    }

    private IEnumerator TransparencyEffectCoroutine()
    {
        isInvulnerable = true;

        // Set transparency level
        Color transparentColor = originalColor;
        transparentColor.a = transparencyLevel;
        characterMaterial.color = transparentColor;

        // Wait for invulnerability to end
        yield return new WaitForSeconds(invulnerabilityDuration);

        // Restore original color and opacity
        characterMaterial.color = originalColor;
        isInvulnerable = false;
    }
}

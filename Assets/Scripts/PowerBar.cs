using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public float itemLifetime;
    public float fadeDuration;

    private PlayerAttack pA;
    private float currentTime;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        pA = FindObjectOfType<PlayerAttack>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > itemLifetime)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (collision.gameObject.CompareTag("Player") && !pA.GetBar())
        {
            pA.CrunchyBar();
            Destroy(gameObject);
        }
    }

    IEnumerator FadeOut()
    {
        Renderer spriteRenderer = gameObject.GetComponent<Renderer>();
        Color originalColor = spriteRenderer.material.color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);  // Gradually reduce alpha
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            spriteRenderer.material.color = newColor;

            yield return null;  // Wait until the next frame
        }

        // Ensure alpha is set to 0 at the end
        spriteRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // Destroy the object after fade-out
        Destroy(gameObject);
    }
}

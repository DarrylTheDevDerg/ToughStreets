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
            ItemDestruction();
        }
    }

    void HealPlayer()
    {
        pS.healthPoints += hpAmount;
    }

    void ItemDestruction()
    {
        SpriteRenderer sR = GetComponent<SpriteRenderer>();
        Color colorRef = sR.color;

        colorRef.a -= 0.01f;

        if (colorRef.a <= 0f)
        {
            Destroy(gameObject);
        }
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

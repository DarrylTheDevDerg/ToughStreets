using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    private PlayerStats pS;
    public int hpAmount;
    public string playerTag;

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

    void HealPlayer()
    {
        pS.healthPoints += hpAmount;
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

        if (player == null)
        {
            EditorGUILayout.HelpBox("You need to input the Player tag for this to work.", MessageType.Error);
        }

        if (amount == 0)
        {
            EditorGUILayout.HelpBox("Amount must be bigger than 0 to work properly.", MessageType.Error);
        }
    }
}

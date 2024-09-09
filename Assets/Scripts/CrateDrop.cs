using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrateDrop : MonoBehaviour
{
    public GameObject[] randomDrop;
    public int amount;

    public void DropItems()
    {
        int objindex = Random.Range(0, randomDrop.Length);

        if (amount != 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(randomDrop[objindex], transform.position, Quaternion.identity);
            }
        }
    }
}

[CustomEditor(typeof(CrateDrop))]
[CanEditMultipleObjects]
public class CDEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        CrateDrop detectionZone = (CrateDrop)target;

        // Check if the Collider is missing
        int amount = detectionZone.amount;

        GameObject[] gO = detectionZone.randomDrop;

        if (gO.Length == 0)
        {
            EditorGUILayout.HelpBox("There must be at least one prefab object in the drop list for the script to work properly!", MessageType.Warning);
        }

        if (amount == 0)
        {
            EditorGUILayout.HelpBox("The amount value must be bigger than 0 for the script to work properly!", MessageType.Warning);
        }
    }
}

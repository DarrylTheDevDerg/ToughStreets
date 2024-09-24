using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] public int enemiesDefeated;

    // Start is called before the first frame update
    public void EnemyDefeat()
    {
        enemiesDefeated++;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(EnemyCount))]
[CanEditMultipleObjects]
public class ECountEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        EnemyCount count = (EnemyCount)target;

        EditorGUILayout.HelpBox("This script only has the purpose to count enemies for other scripts that require some form of measurement.", MessageType.Info);
    }
}

#endif
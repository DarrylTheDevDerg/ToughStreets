using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimedObjectActivation : MonoBehaviour
{
    public GameObject[] objects;
    public float threshold;
    private float time;

    public bool onStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (onStart && time > threshold)
        {
            ActivateObjects();
        }
    }

    public void ActivateObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }
}

[CustomEditor(typeof(TimedObjectActivation))]
[CanEditMultipleObjects]
public class TOAEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        TimedObjectActivation detectionZone = (TimedObjectActivation)target;

        // Check if the Collider is missing
        float amount = detectionZone.threshold;

        GameObject[] gO = detectionZone.objects;

        if (gO.Length == 0)
        {
            EditorGUILayout.HelpBox("There must be at least one prefab object in the drop list for the script to work properly!", MessageType.Warning);
        }

        if (amount == 0)
        {
            EditorGUILayout.HelpBox("The threshold will delay the activation of the objects by a certain value of time calculated with DeltaTime, if the value is 0, the objects will activate immediately as the function is called..", MessageType.Info);
        }
    }
}
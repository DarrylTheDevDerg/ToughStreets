using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventCollision : MonoBehaviour
{
    public UnityEvent[] events;
    public string playerTag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            foreach (var e in events)
            {
                e.Invoke();
            }
        }
    }
}

[CustomEditor(typeof(EventCollision))]
[CanEditMultipleObjects]
public class ECEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        EventCollision detectionZone = (EventCollision)target;

        // Check if the Collider is missing
        Collider collider = detectionZone.GetComponent<Collider>();

        if ((collider == null || !collider.isTrigger) || (collider != null && !collider.isTrigger))
        {
            EditorGUILayout.HelpBox("Error: This script requires a Trigger Collider, which is missing!", MessageType.Error);

            if (GUILayout.Button("Add BoxCollider?"))
            {
                detectionZone.gameObject.AddComponent<BoxCollider>();
                detectionZone.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }
    }
}

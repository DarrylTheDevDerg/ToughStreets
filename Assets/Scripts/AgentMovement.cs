#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public Vector3[] waypoints; // Array of points the agent will move to
    private int currentWaypointIndex = 0;

    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Grabs the agent component
        MoveToNextWaypoint();
    }

    void Update()
    {
        // Check if the agent has reached the current destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return; // If no waypoints, exit

        // Set the agent to go to the next waypoint
        agent.SetDestination(waypoints[currentWaypointIndex]);

        // Cycle through the waypoints
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Vector3))]
public class Vector3XZDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Start drawing the property field
        EditorGUI.BeginProperty(position, label, property);

        // Display label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Calculate rects for X and Z
        Rect xRect = new Rect(position.x, position.y, position.width / 2 - 10, position.height);
        Rect zRect = new Rect(position.x + position.width / 2 + 10, position.y, position.width / 2 - 10, position.height);

        // Fetch X and Z values
        SerializedProperty xProp = property.FindPropertyRelative("x");
        SerializedProperty zProp = property.FindPropertyRelative("z");

        // Lock the Y value to a certain number (let's say 0)
        property.FindPropertyRelative("y").floatValue = 0f;

        // Draw fields for X and Z
        EditorGUI.PropertyField(xRect, xProp, GUIContent.none);
        EditorGUI.PropertyField(zRect, zProp, GUIContent.none);

        EditorGUI.EndProperty();
    }
}

#endif


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public GameObject[] spawnObjects;
    public int amount;

    public bool shouldDelay;
    public float delayAmt;

    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!shouldDelay)
        {
            SpawnItems();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldDelay)
        {
            currentTime += Time.deltaTime;
        }

        if (currentTime > delayAmt)
        {
            SpawnItems();
        }    
    }

    public void SpawnItems()
    {
        // Get a random spawn position within the bounds of the enemy spawner
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn randomAmount of enemies from the enemyPrefabs array
        for (int i = 0; i < amount; i++)
        {
            if (spawnObjects.Length > 1)
            {
                int randomPrefabIndex = Random.Range(0, spawnObjects.Length);
                GameObject randomgO = Instantiate(spawnObjects[randomPrefabIndex], spawnPosition, Quaternion.identity);
            }

            GameObject go = Instantiate(spawnObjects[0], spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 CalculateSpawnerSize()
    {
        // Get the size of the enemy spawner's collider in all three dimensions
        Vector3 size = transform.GetComponent<Collider>().bounds.size;
        return size;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnerSize = transform.GetComponent<Collider>().bounds.size;

        // Calculate random X, Y, and Z positions within the bounds of the enemy spawner
        float randomX = Random.Range(-spawnerSize.x / 2f, spawnerSize.x / 2f);
        float randomY = Random.Range(-spawnerSize.y / 2f, spawnerSize.y / 2f);
        float randomZ = Random.Range(-spawnerSize.z / 2f, spawnerSize.z / 2f);

        // Get the position of the enemy spawner
        Vector3 spawnerPosition = transform.position;

        // Create a vector with the random X, Y, and Z positions within the spawner's bounds
        return spawnerPosition + new Vector3(randomX, randomY, randomZ);
    }
}

[CustomEditor(typeof(CrateSpawner))]
[CanEditMultipleObjects]
public class CSEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        CrateSpawner detectionZone = (CrateSpawner)target;

        // Check if the Collider is missing
        Collider collider = detectionZone.GetComponent<Collider>();

        if (collider == null)
        {
            EditorGUILayout.HelpBox("Error: No Collider component detected! This script requires a Collider to function properly.", MessageType.Error);

            if (GUILayout.Button("Add Trigger BoxCollider?"))
            {
                detectionZone.gameObject.AddComponent<BoxCollider>();
                detectionZone.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }

        if (detectionZone.spawnObjects == null)
        {
            EditorGUILayout.HelpBox("Error: This script requires at least ONE prefab to work properly!", MessageType.Error);
        }

        EditorGUILayout.HelpBox("Please, do not use the delay options if not necessary, as it can throttle performance.", MessageType.Info);
    }
}

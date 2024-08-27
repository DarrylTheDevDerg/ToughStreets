using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public int[] amountPerRound;
    public int[] advanceAmt;

    public int rounds;
    public float spawnCooldown;

    private EnemyCount eC;
    private int roundCount;
    private int currentAmt;

    private float currentTime = 0;

    private void Start()
    {
        eC = FindObjectOfType<EnemyCount>();
    }

    private void Update()
    {
        currentAmt = eC.enemiesDefeated;

        // Check if there are more rounds to spawn enemies
        if (roundCount < rounds)
        {
            currentTime += Time.deltaTime;

            // Check if it's time to spawn enemies
            if (currentTime > spawnCooldown)
            {
                // Spawn enemies
                SpawnEnemies();

            }
        }

        if (currentAmt > amountPerRound[roundCount])
        {
            currentAmt = 0;
            roundCount++;
        }

        if (roundCount >= rounds)
        {
            Optimization();
        }
    }

    private void SpawnEnemies()
    {
        // Get a random spawn position within the bounds of the enemy spawner
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Spawn randomAmount of enemies from the enemyPrefabs array
        for (int i = 0; i < amountPerRound[roundCount]; i++)
        {
            if (currentTime > spawnCooldown)
            {
                int randomPrefabIndex = Random.Range(0, enemies.Length);
                GameObject go = Instantiate(enemies[randomPrefabIndex], spawnPosition, Quaternion.identity);
                currentTime = 0;
            }
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

    void Optimization()
    {
        Destroy(gameObject);
    }
}

[CustomEditor(typeof(EnemySpawner))]
public class DetectionZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        EnemySpawner detectionZone = (EnemySpawner)target;

        // Check if the Collider is missing
        Collider collider = detectionZone.GetComponent<Collider>();

        if (collider == null)
        {
            EditorGUILayout.HelpBox("Error: No Collider component detected! This script requires a Collider to function properly.", MessageType.Error);

            if (GUILayout.Button("Add CapsuleCollider"))
            {
                detectionZone.gameObject.AddComponent<CapsuleCollider>();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public int[] amountPerRound;
    public int rounds;
    public float spawnCooldown;

    private EnemyCount eC;
    private int roundCount;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Optimization()
    {
        Destroy(gameObject);
    }

    void RoundSpawn()
    {

    }
}

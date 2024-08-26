using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP;
    private int atkAmount;

    public int lowAtkAmount;
    public int highAtkAmount;

    private PlayerStats pS;
    private EnemyCount eC;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        eC = GetComponent<EnemyCount>();

        atkAmount = Random.Range(lowAtkAmount, highAtkAmount);   
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP < 0)
        {
            OnDeath();
        }
    }

    void PlayerHarm()
    {
        pS.healthPoints -= atkAmount;
    }

    void OnDeath()
    {
        eC.EnemyDefeat();
    }
}
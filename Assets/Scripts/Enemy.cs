using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP;
    private int atkAmount;

    public int lowAtkAmount;
    public int highAtkAmount;

    private PlayerStats pS;
    private EnemyCount eC;
    private EnemySpawner eS;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        eC = FindObjectOfType<EnemyCount>();
        eS = FindObjectOfType<EnemySpawner>();

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
        eS.currentAmt += 1;
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHarm();
        }
    }
}

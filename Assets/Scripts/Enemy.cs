using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP;
    public bool isBoss;
    public string dedTrigger;
    private int atkAmount;

    public int lowAtkAmount;
    public int highAtkAmount;

    private PlayerStats pS;
    private EnemyCount eC;
    private EnemySpawner eS;
    private Animator an;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        eC = FindObjectOfType<EnemyCount>();
        eS = FindObjectOfType<EnemySpawner>();
        an = FindObjectOfType<Animator>();

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
        an.SetTrigger(dedTrigger);
    }

    public void Optimization()
    {
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

    public int GetStat(string key)
    {
        if (isBoss)
        {
            switch (key)
            {
                case "enemyHP":
                    return (int)enemyHP;

                default:
                    Debug.LogError("No value specified, defaulting to 0.");
                    return 0;
            }
        }

        return 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHP;
    public string dedTrigger, hurtTrigger;
    private int atkAmount;

    public int lowAtkAmount;
    public int highAtkAmount;

    private PlayerStats pS;
    private EnemyCount eC;
    private EnemySpawner eS;
    private Animator an;
    private FollowPlayer fP;
    private TransparencyEffect tE;

    private void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        eC = FindObjectOfType<EnemyCount>();
        eS = FindObjectOfType<EnemySpawner>();
        tE = FindObjectOfType<TransparencyEffect>();

        fP = GetComponent<FollowPlayer>();
        an = GetComponent<Animator>();

        atkAmount = Random.Range(lowAtkAmount, highAtkAmount);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHarm()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Animator pAn = player.GetComponent<Animator>();
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();

        pAn.SetTrigger(playerAttack.hurtAnimName);
        pS.healthPoints -= atkAmount;
        tE.TriggerInvulnerability();
    }

    void OnDeath()
    {
        fP.enabled = false;
        Collider collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;

        an.SetTrigger(dedTrigger);

        if (eC != null && eS != null)
        {
            eC.EnemyDefeat();
            eS.currentAmt += 1;
        }
    }

    public void Optimization()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;
        an.SetTrigger(hurtTrigger);

        if (enemyHP <= 0)
        {
            enemyHP = 0;
            OnDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerAttack pA = player.GetComponent<PlayerAttack>();

        if (collision.transform.CompareTag("Player") && !tE.getInvulnerable())
        {
            PlayerHarm();
        }
    }
}

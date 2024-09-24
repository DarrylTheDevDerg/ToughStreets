using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDependentTrigger : MonoBehaviour
{
    private Boss main;
    private Enemy en;
    private EnemyCount eC;

    public GameObject[] objects;
    public bool useEC;

    public int enemyCountNeeded;

    // Start is called before the first frame update
    void Start()
    {
        main = FindObjectOfType<Boss>();
        en = FindObjectOfType<Enemy>();
        eC = GetComponent<EnemyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (en.enemyHP <= 0)
        {
            if (useEC && eC.enemiesDefeated >= enemyCountNeeded)
                foreach (GameObject obj in objects)
                {
                    obj.SetActive(true);
                }
        }
    }
}

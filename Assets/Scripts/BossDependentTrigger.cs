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
        en = FindObjectOfType<Enemy>();
        eC = GetComponent<EnemyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        main = FindObjectOfType<Boss>();

        if (en != null && en.enemyHP <= 0 && main != null)
        {
            if (useEC && eC.enemiesDefeated >= enemyCountNeeded)
                foreach (GameObject obj in objects)
                {
                    obj.SetActive(true);
                }
        }
    }
}

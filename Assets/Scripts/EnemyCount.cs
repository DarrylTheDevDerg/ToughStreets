using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] public int enemiesDefeated;

    // Start is called before the first frame update
    public void EnemyDefeat()
    {
        enemiesDefeated++;
    }
}

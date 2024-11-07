using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCountEvent : MonoBehaviour
{
    public UnityEvent[] actions;
    public int needed;
    private EnemyCount eC;

    public bool mustDoUpdate;

    // Start is called before the first frame update
    void Start()
    {
        eC = FindObjectOfType<EnemyCount>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mustDoUpdate)
        {
            EventRealization();
        }
    }

    void EventRealization()
    {
        if (eC.enemiesDefeated >= needed)
        {
            foreach (UnityEvent e in actions)
            {
                e.Invoke();
            }
        }
    }
}

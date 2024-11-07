using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    private Boss main;
    private BossDisplay bD;

    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<Boss>();

        if (main == null)
        {
            bD.enabled = false;
        }
        else
        {
            bD.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossDisplay bD;

    // Start is called before the first frame update
    void Start()
    {
        bD = FindObjectOfType<BossDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        bD.enabled = false;
    }
}

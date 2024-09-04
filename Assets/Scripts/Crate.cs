using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public float crateHP;

    private PlayerAttack pA;
    private CrateDrop cD;

    // Start is called before the first frame update
    void Start()
    {
        pA = FindObjectOfType<PlayerAttack>();
        cD = GetComponent<CrateDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (crateHP <= 0)
        {
            cD.DropItems();
            Destroy(gameObject);
        }
    }
}

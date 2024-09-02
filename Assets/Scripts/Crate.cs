using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public float crateHP;
    private float currentHP;

    private PlayerAttack pA;

    // Start is called before the first frame update
    void Start()
    {
        pA = FindObjectOfType<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

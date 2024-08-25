using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float quickFireWindow;
    
    public float damageAmount;
    public int comboAmount;

    private float quickFireCooldown;
    private PlayerAnimation pA;

    private CapsuleCollider atkCol;

    // Start is called before the first frame update
    void Awake()
    {
        pA = GetComponent<PlayerAnimation>();
        atkCol = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttacks();

    }

    void ComboAttacks()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pA.Punch1();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            pA.Kick1();
        }
    }

    void AttackDetect()
    {

    }
}

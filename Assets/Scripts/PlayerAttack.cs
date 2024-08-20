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


    // Start is called before the first frame update
    void Awake()
    {
        pA = GetComponent<PlayerAnimation>();
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

        }
    }
}

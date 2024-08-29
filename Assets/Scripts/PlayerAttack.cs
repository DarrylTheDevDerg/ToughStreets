using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float comboDelay;
    public float damageAmount;
    public int comboAmount;
    public string enemyTag;

    private Animator animator;
    private Collider atkCol;
    private float lastAttackTime; // Tiempo del último ataque
    private int comboStep;        // Paso actual del combo

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        atkCol = GetComponent<Collider>();
        comboStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            HandleCombo();
        }
    }

    private void HandleCombo()
    {
        float currentTime = Time.time;

        // Si ha pasado suficiente tiempo desde el último ataque, reinicia el combo
        if (currentTime - lastAttackTime > comboDelay)
        {
            comboStep = 0;
            comboAmount = 0;
        }

        // Ejecuta el ataque correspondiente según el paso del combo
        switch (comboStep)
        {
            case 0:
                // Realiza el primer ataque del combo
                animator.SetBool("Combo1", true);
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo2", true);
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo3", true);
                break;
        }

        // Actualiza el paso del combo y el tiempo del último ataque
        comboStep = (comboStep + 1) % 3; // Cambia al siguiente ataque en el combo (0, 1, 2, 0, 1, 2, ...)
        lastAttackTime = currentTime;
    }

    private void ResetComboBools()
    {
        // Espera el tiempo necesario para que las animaciones se reproduzcan completamente
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
        animator.SetBool("Combo3", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the triggering collider is the specific one
        if (other == atkCol)
        {
            // Check if the object we're colliding with has the "Enemy" tag
            if (other.CompareTag("Enemy"))
            {
                // Try to get the Enemy script attached to the collided object
                Enemy enemy = other.GetComponent<Enemy>();

                if (enemy != null)
                {
                    // Reduce the enemy's HP
                    enemy.TakeDamage();
                }
            }
        }
    }
}

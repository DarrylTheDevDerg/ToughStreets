using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float comboDelay, damageAmount;
    public int comboAmount;
    public string enemyTag, crateTag, hurtAnimName;

    private Animator animator;
    private float lastAttackTime; // Tiempo del �ltimo ataque
    private int comboStep;        // Paso actual del combo
    private BlinkOnDamage boD;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boD = GetComponent<BlinkOnDamage>();
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

        // Si ha pasado suficiente tiempo desde el �ltimo ataque, reinicia el combo
        if (currentTime - lastAttackTime > comboDelay)
        {
            comboStep = 0;
            comboAmount = 0;
            lastAttackTime = 0;
            ResetComboBools();
        }

        // Ejecuta el ataque correspondiente seg�n el paso del combo
        switch (comboStep)
        {
            case 0:
                // Realiza el primer ataque del combo
                animator.SetBool("Combo1", true);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", false);
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", true);
                animator.SetBool("Combo3", false);
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", true);
                break;
        }

        // Actualiza el paso del combo y el tiempo del �ltimo ataque
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

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(enemyTag))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
            other.gameObject.GetComponent<BlinkOnDamage>().TriggerBlink();
            comboAmount++;
        }

        if (other.transform.CompareTag(crateTag))
        {
            other.gameObject.GetComponent<Crate>().TakeDamage(damageAmount);
            other.gameObject.GetComponent<BlinkOnDamage>().TriggerBlink();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            animator.SetTrigger(hurtAnimName);
            boD.TriggerBlink();
            comboAmount = 0;
        }
    }

    public int GetStat(string key)
    {
        switch (key)
        {
            case "comboAmount":
                return comboAmount;

            default:
                Debug.LogWarning("No key attached or valid key detected, ignoring value.");
                return 0;
        }
    }
}

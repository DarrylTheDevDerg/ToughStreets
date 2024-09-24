using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public float comboDelay, damageAmount;
    public int comboAmount, iFrames, invul;
    public string enemyTag, crateTag, hurtAnimName;
    public TextMeshProUGUI comboText;
    public GameObject comboUI;

    private Animator animator;
    private float lastAttackTime; // Tiempo del último ataque
    private int comboStep;        // Paso actual del combo
    private BlinkOnDamage boD;
    private bool attacking;
    private VariableTextModify vtm;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boD = GetComponent<BlinkOnDamage>();
        vtm = FindObjectOfType<VariableTextModify>();

        comboStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (comboAmount > 1)
        {
            comboUI.SetActive(true);
            comboText.text = comboAmount.ToString();
            vtm.enabled = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleCombo();
            
        }

        if (invul > 0 && invul != 0)
        {
            float degrade = 0; 
            degrade += Time.deltaTime;

            if (degrade > 2)
            {
                invul--;
            }
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
            lastAttackTime = 0;
            comboUI.SetActive(false);
            ResetComboBools();
            attacking = false;
            vtm.enabled = false;
        }

        // Ejecuta el ataque correspondiente según el paso del combo
        switch (comboStep)
        {
            case 0:
                // Realiza el primer ataque del combo
                animator.SetBool("Combo1", true);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", false);
                attacking = true;
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", true);
                animator.SetBool("Combo3", false);
                attacking = true;
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", true);
                attacking = true;
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
        attacking = false;
        vtm.enabled= false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(enemyTag) && attacking && !other.GetComponent<FollowPlayer>().attacking)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damageAmount);
            other.gameObject.GetComponent<BlinkOnDamage>().TriggerBlink();
            comboAmount++;
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

    public void SetInvFrames()
    {
        invul = iFrames;
    }
}

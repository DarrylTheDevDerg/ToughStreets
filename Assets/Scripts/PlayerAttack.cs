using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public float comboDelay, damageAmount;
    public int comboAmount;
    public string enemyTag, crateTag, hurtAnimName;
    public TextMeshProUGUI comboText;
    public GameObject comboUI;

    private Animator animator; // Tiempo del último ataque
    private int comboStep;        // Paso actual del combo
    private BlinkOnDamage boD;
    private bool attacking;
    private VariableTextModify vtm;
    private TransparencyEffect tE;
    private float currentTime, currentDamage, lastAttackTime;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boD = GetComponent<BlinkOnDamage>();
        vtm = FindObjectOfType<VariableTextModify>();

        tE = GetComponent<TransparencyEffect>();

        currentDamage = damageAmount;
        comboStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

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
    }

    private void HandleCombo()
    {
        // Si ha pasado suficiente tiempo desde el último ataque, reinicia el combo
        if (lastAttackTime > comboDelay)
        {
            ComboReset();
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
                currentDamage = damageAmount;
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", true);
                animator.SetBool("Combo3", false);
                attacking = true;
                currentDamage = currentDamage * 1.05f;
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", true);
                attacking = true;
                currentDamage = currentDamage * 1.12f;
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
            other.gameObject.GetComponent<Enemy>().TakeDamage(currentDamage);
            other.gameObject.GetComponent<BlinkOnDamage>().TriggerBlink();
            comboAmount++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == enemyTag && !tE.getInvulnerable())
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

    public void ComboReset()
    {
        comboStep = 0;
        comboAmount = 0;
        lastAttackTime = 0;
        comboUI.SetActive(false);

        ResetComboBools();
        attacking = false;
        vtm.enabled = false;
    }
}

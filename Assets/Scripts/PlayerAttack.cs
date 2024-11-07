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
    private int comboStep, rumbleDmg;        // Paso actual del combo
    private BlinkOnDamage boD;
    private bool attacking, powerBar, rumbleMode;
    private TransparencyEffect tE;
    private float currentTime, currentDamage, powerBarLife;
    private PlayerStats pS;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boD = GetComponent<BlinkOnDamage>();

        tE = GetComponent<TransparencyEffect>();
        pS = GetComponent<PlayerStats>();

        currentDamage = damageAmount;
        comboStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (pS.healthPoints <= 25)
        {
            rumbleMode = true;
        }
        else
        {
            rumbleMode = false;
        }

        if (comboAmount > 1)
        {
            comboUI.SetActive(true);
            comboText.text = comboAmount.ToString();
            currentTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            HandleCombo();
        }

        if (powerBar)
        {
            powerBarLife += Time.deltaTime;

            if (powerBarLife > 10)
            {
                powerBar = false;
            }
        }

        if (rumbleMode)
        {
            rumbleDmg = 2;
        }
        else
        {
            rumbleDmg = 0;
        }
    }

    private void HandleCombo()
    {
        // Si ha pasado suficiente tiempo desde el último ataque, reinicia el combo
        if (currentTime > comboDelay)
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
                if (powerBar)
                {
                    currentDamage = (damageAmount * 1.5f) + rumbleDmg;
                }
                else
                {
                    currentDamage = damageAmount + rumbleDmg;
                }
                
                break;
            case 1:
                // Realiza el segundo ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", true);
                animator.SetBool("Combo3", false);
                attacking = true;
                if (powerBar)
                {
                    currentDamage = ((damageAmount * 1.5f) * 1.05f) + rumbleDmg;
                }
                else
                {
                    currentDamage = (damageAmount * 1.05f) + rumbleDmg;
                }
                break;
            case 2:
                // Realiza el tercer ataque del combo
                animator.SetBool("Combo1", false);
                animator.SetBool("Combo2", false);
                animator.SetBool("Combo3", true);
                attacking = true;
                if (powerBar)
                {
                    currentDamage = ((damageAmount * 1.5f) * 1.12f) + rumbleDmg;
                }
                else
                {
                    currentDamage = (damageAmount * 1.12f) + rumbleDmg;
                }
                break;
        }

        // Actualiza el paso del combo y el tiempo del último ataque
        comboStep = (comboStep + 1) % 3; // Cambia al siguiente ataque en el combo (0, 1, 2, 0, 1, 2, ...)
        currentTime = 0;
    }

    private void ResetComboBools()
    {
        // Espera el tiempo necesario para que las animaciones se reproduzcan completamente
        animator.SetBool("Combo1", false);
        animator.SetBool("Combo2", false);
        animator.SetBool("Combo3", false);

        attacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(enemyTag) && attacking && !other.GetComponent<FollowPlayer>().attacking)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(currentDamage);
            other.gameObject.GetComponent<BlinkOnDamage>().TriggerBlink();
            comboAmount++;
        }

        if (other.transform.CompareTag(crateTag) && attacking)
        {
            other.gameObject.GetComponent<Crate>().TakeDamage(currentDamage);
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
            ComboReset();
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
        currentTime = 0;
        comboUI.SetActive(false);

        ResetComboBools();
        attacking = false;
    }

    public void CrunchyBar()
    {
        powerBar = true;
    }

    public bool GetBar()
    {
        return powerBar;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossDisplay : MonoBehaviour
{
    public TextMeshProUGUI bossHp;
    public TextMeshProUGUI bossName;
    public GameObject bossUiDisplay;
    public bool checkUpdate;

    private Boss bossEn;
    private Enemy en;

    // Start is called before the first frame update
    void Start()
    {
        bossEn = FindObjectOfType<Boss>();

        if (bossEn != null)
        {
            ActivateUIRelatedElements();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEn == null && checkUpdate)
        {
            bossEn = FindObjectOfType<Boss>();
        }

        if (bossEn != null)
        {
            if (!bossUiDisplay.activeSelf)
            {
                ActivateUIRelatedElements();
            }
            
            SetHPText();

            if (en.enemyHP <= 0)
            {
                bossUiDisplay.SetActive(false);
            }
        }
    }

    public void SetHPText()
    {
        if (bossHp != null && bossUiDisplay)
        {
            bossHp.text = ((int)en.enemyHP).ToString();
        }
    }

    public void ActivateUIRelatedElements()
    {
        bossUiDisplay.SetActive(true);
        en = bossEn.GetComponent<Enemy>();

        if (bossUiDisplay)
        {
            bossName.text = bossEn.name;
        }
    }
}

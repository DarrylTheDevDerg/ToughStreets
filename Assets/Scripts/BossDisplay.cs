using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossDisplay : MonoBehaviour
{
    public TextMeshProUGUI bossHp;
    public TextMeshProUGUI bossName;
    public GameObject bossUiDisplay;

    private Boss bossEn;

    // Start is called before the first frame update
    void Start()
    {
        bossEn = FindObjectOfType<Boss>();

        if (bossEn != null)
        {
            bossUiDisplay.SetActive(true);
        }

        if (bossHp != null && bossUiDisplay)
        {
            bossName.text = bossEn.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEn != null)
        {
            SetHPText();
        }
        
        if (bossHp == null)
        {
            bossUiDisplay.SetActive(false);
        }
    }

    public void SetHPText()
    {
        if (bossHp != null && bossUiDisplay)
        {
            bossHp.text = bossEn.GetComponent<Enemy>().enemyHP.ToString();
        }
    }
}

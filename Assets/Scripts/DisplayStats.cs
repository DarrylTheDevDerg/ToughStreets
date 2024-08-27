using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI levelDisplay;

    private PlayerStats pS;
    private string levelName;

    // Start is called before the first frame update
    void Start()
    {
        pS = FindObjectOfType<PlayerStats>();
        levelName = SceneManager.GetActiveScene().name;

        switch (levelName)
        {
            case "Level1":
                levelDisplay.text = "Tutorial";
                break;
            case "Level2":
                levelDisplay.text = "Intermediate";
                break;
            case "Level3":
                levelDisplay.text = "The End";
                break;
            default:
                levelDisplay.text = "null";
                break;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHP();
    }

    void UpdateHP()
    {
        healthText.text = pS.healthPoints.ToString();
    }
}

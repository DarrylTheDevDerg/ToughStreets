using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    public string[] levelNames;
    public string endScreen;
    public int currentLvl;

    private PlayerPrefsManager ppm;

    private void Start()
    {
        ppm = GetComponent<PlayerPrefsManager>();
        currentLvl = PlayerPrefs.GetInt("Current Level");
    }

    public void Progression()
    {
        string lvlName = SceneManager.GetActiveScene().name;

        switch (lvlName)
        {
            case "Level1":
                currentLvl = 1;
                PlayerPrefs.SetInt("Current Level", 1);
                ppm.SaveAll();
                SceneManager.LoadScene(levelNames[currentLvl]);
                break;

            case "Level2":
                currentLvl = 2;
                PlayerPrefs.SetInt("Current Level", 2);
                ppm.SaveAll();
                SceneManager.LoadScene(levelNames[currentLvl]);
                break;

            case "Level3":
                currentLvl = 2;
                PlayerPrefs.SetInt("Game Finished", 1);
                SceneManager.LoadScene(endScreen);
                break;
        }
    }

    public void NewGame()
    {
        currentLvl = 0;
        PlayerPrefs.SetInt("Current Level", 0);
        PlayerPrefs.SetInt("Game Finished", 0);
        PlayerPrefs.SetInt("Score", 0);
        ppm.SaveAll();
        SceneManager.LoadScene(levelNames[currentLvl]);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(levelNames[currentLvl]);
    }
}

[CustomEditor(typeof(LevelProgress))]
[CanEditMultipleObjects]
public class LPEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Get a reference to the DetectionZone script
        LevelProgress detectionZone = (LevelProgress)target;

        // Check if the Collider is missing
        string cS = detectionZone.endScreen;

        string[] lN = detectionZone.levelNames;

        if (lN.Length == 0)
        {
            EditorGUILayout.HelpBox("There must be at least one string in the list for the script to work properly!", MessageType.Warning);
        }

        if (cS == null)
        {
            EditorGUILayout.HelpBox("Credits scene name must not be empty!", MessageType.Error);
        }
    }
}

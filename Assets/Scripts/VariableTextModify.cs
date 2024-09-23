using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class VariableTextModify : MonoBehaviour
{
    public string valueName;
    public TextMeshProUGUI text;

    public bool isItPlayer;
    public bool isItBoss;

    private PlayerAttack pA;
    private Enemy en;

    // Start is called before the first frame update
    void Start()
    {
        pA = FindObjectOfType<PlayerAttack>();

        if (en.isBoss)
        {
            en = FindObjectOfType<Enemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ReflectValue();
    }

    public void ReflectValue()
    {
        if (isItPlayer)
        {
            text.text = pA.GetStat(valueName).ToString();
        }
        else if (isItBoss)
        {
            if (en.isBoss)
            {
                text.text = en.GetStat(valueName).ToString();
            }
        }
        
    }
}

[CustomEditor(typeof(VariableTextModify))]
public class VTMEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Reference the target (BoolToggle script)
        VariableTextModify toggle = (VariableTextModify)target;

        // Create toggles for OptionA and OptionB
        toggle.isItPlayer = EditorGUILayout.Toggle("Should reflect Player values?", toggle.isItPlayer);
        toggle.isItBoss = EditorGUILayout.Toggle("Should reflect enemy / Boss values?", toggle.isItBoss);

        // Automatically deactivate the other when one is selected
        if (toggle.isItPlayer) toggle.isItBoss = false;
        if (toggle.isItBoss) toggle.isItPlayer = false;

        // Apply changes
        EditorUtility.SetDirty(target);
    }
}
